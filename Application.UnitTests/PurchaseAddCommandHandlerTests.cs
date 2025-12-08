using Application.Contracts.Purchases.Commands.Add;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Enums;
using Domain.Errors;
using Domain.Models;
using Domain.Shared;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.UnitTests.Purchases
{
    public class PurchaseAddCommandHandlerTests
    {
        private readonly Mock<IPurchaseRepository> _mockPurchaseRepo;
        private readonly Mock<IItemRepository> _mockItemRepo;
        private readonly Mock<ITopCategoryCalculator> _mockTopCategoryCalc;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly PurchaseAddCommandHandler _handler;

        // Тестовые данные
        private readonly Item _testItem = new(
            id: Guid.NewGuid(),
            name: "Test Item",
            category: ItemCategory.Jewelry,
            price: 100.00m,
            stock: 5
        );

        public PurchaseAddCommandHandlerTests()
        {
            _mockPurchaseRepo = new Mock<IPurchaseRepository>();
            _mockItemRepo = new Mock<IItemRepository>();
            _mockTopCategoryCalc = new Mock<ITopCategoryCalculator>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();

            _handler = new PurchaseAddCommandHandler(
                _mockPurchaseRepo.Object,
                _mockItemRepo.Object,
                _mockTopCategoryCalc.Object,
                _mockUnitOfWork.Object
            );
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task Handle_InvalidQuantity_ReturnsValidationFailure(int invalidQuantity)
        {
            // Arrange

            var command = new PurchaseAddCommand(_testItem.Id, invalidQuantity);
            _mockItemRepo.Setup(repo => repo.GetByIdAsync(_testItem.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(_testItem));

            _mockItemRepo.Setup(repo => repo.ReduceStockAsync(_testItem.Id, invalidQuantity, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure(ApplicationErrors.Item.PriceInvalid));

            _mockPurchaseRepo.Setup(repo => repo.AddAsync(It.IsAny<Purchase>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(Guid.NewGuid()));

            _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<Result<Guid>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(Guid.NewGuid()));

            _mockTopCategoryCalc.Setup(calc => calc.CalculateTopCategoryAsync())
                .ReturnsAsync(new Domain.Config.TopCategoryConfig());

            var validator = new PurchaseAddCommandValidator();
            var validationResult = validator.Validate(command);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be(ApplicationErrors.Item.PriceInvalid.Message);
        }
    }
}
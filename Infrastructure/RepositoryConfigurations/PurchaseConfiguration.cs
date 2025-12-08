using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.RepositoryConfigurations
{
	internal class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
	{
		public void Configure(EntityTypeBuilder<Purchase> builder)
		{
			builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Item)
                .WithMany(i => i.Purchases)
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
	}
}

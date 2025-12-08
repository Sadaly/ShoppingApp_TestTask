namespace Domain.Enums
{
    // Решил использовать enum. Если необходим функционал,
    // который изменял бы этот список в ходе работы программы,
    // то лучше создать отдельную сущность и связать её через 
    // CategoryId
    public enum ItemCategory
    {
        None,
        Jewelry,
        Clothes,
        Food,
    }
}

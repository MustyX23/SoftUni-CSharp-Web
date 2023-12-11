namespace ShoppingList.Data.Models
{
    public class ProductNote
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}

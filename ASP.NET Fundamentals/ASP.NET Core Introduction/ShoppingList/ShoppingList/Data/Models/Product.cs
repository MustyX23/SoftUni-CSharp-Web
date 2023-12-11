namespace ShoppingList.Data.Models
{
    public class Product
    {
        public Product()
        {
            ProductNotes = new List<ProductNote>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public IList<ProductNote> ProductNotes { get; set; } = null!;
    }

}

namespace AdminStore.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Relacionamento: Uma categoria pode ter muitos produtos
        public ICollection<Product> Products { get; set; }
    }
}
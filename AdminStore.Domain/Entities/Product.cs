namespace AdminStore.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        // Relacionamento: Um produto pertence a uma categoria
        public int CategoryId { get; set; } // Chave estrangeira
        public Category Category { get; set; } // Propriedade de navegação
    }
}
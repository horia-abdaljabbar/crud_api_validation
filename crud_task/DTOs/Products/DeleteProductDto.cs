namespace crudTask.DTOs.Product
{
    public class DeleteProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}

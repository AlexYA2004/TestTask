using System.ComponentModel.DataAnnotations;

namespace TestTask.Entities
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();

            IsAvailaleToOrder = true;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool IsAvailaleToOrder { get; set; }

        public ICollection<ProductAndOrder> ProductsAndOrder { get; set; }
    }
}

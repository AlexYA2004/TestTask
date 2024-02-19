using System.ComponentModel.DataAnnotations;

namespace TestTask.Entities
{
    public class ProductAndOrder
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }
    }
}

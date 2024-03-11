using System.ComponentModel.DataAnnotations;

namespace TestTask.Entities
{
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();

            CreatedDate = DateTime.Now;
        }


        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public ICollection<ProductAndOrder> ProductsAndOrder { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}

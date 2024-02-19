using System.ComponentModel.DataAnnotations;

namespace TestTask.Entities
{
    public class OrderInfo
    {
        [Required]
        public IEnumerable<ProductInfo> ProductsInOrder { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
    }
}

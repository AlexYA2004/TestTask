using System.ComponentModel.DataAnnotations;


namespace TestTask.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<Order> Orders { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}

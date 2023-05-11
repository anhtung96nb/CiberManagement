namespace CiberManagement.DAL.Model
{
    using System.ComponentModel.DataAnnotations;
    public class Customer
    {

        [Required]
        public Guid ID { get; set; }

        [Required,MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(256)]
        public string Address { get; set; } = null!;

        public virtual ICollection<Order>? Orders { get; set; }

    }
}

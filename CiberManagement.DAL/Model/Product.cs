namespace CiberManagement.DAL.Model
{
    using System.ComponentModel.DataAnnotations;
    public class Product
    {


        [Key]
        public Guid ID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
        
        [Required,Range(0,double.MaxValue)]
        public double Price { get; set; }

        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0;

        public Guid? CategoryID { get; set; }
        public Category? Category { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace CiberManagement.DAL.Model 
{
    public class Order
    {
        [Key]
        public Guid ID { get; set; }
        public Guid? CustomerID { get; set; }
        public Customer? Customer { get; set; }
        public Guid? ProductID { get; set; }
        public Product? Product { get; set; }

        [Required,Range(0, double.MaxValue)]
        public double Amount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

namespace CiberManagement.DAL.Model
{
    using System.ComponentModel.DataAnnotations;
    public class Category
    {

        [Key]
        public Guid ID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public string Descriptions { get; set; }

        public IEnumerable<Product>? Products { get; set; }

    }
}

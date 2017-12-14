using System.ComponentModel.DataAnnotations;

namespace ProductCatalogMVC.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Name required")]
        [Display(Name = "Product")]
        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(500)]
        public string Description { get; set; }

        [Display(Name = "Price/Unit")]
        public double PricePerUnit { get; set; }
    }
}

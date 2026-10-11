using System.ComponentModel.DataAnnotations;

namespace BARAPP.Models
{
    public class Shop
    {
        [Required]
        public string ShopName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

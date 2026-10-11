using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BARAPP.Models
{
    public class Category
    {
        [Required]
        [DisplayName("Category Name")]
        public string categoryname { get; set; }
        [Required]
        public string Description { get; set; }
    }
}

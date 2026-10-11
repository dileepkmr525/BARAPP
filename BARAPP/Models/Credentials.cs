using System.ComponentModel.DataAnnotations;

namespace BARAPP.Models
{
    public class Credentials
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

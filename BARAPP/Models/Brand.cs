using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BARAPP.Models
{
    public class Brand
    {
        [Required]
        [DisplayName("Brand Name")]
        public string BrandName { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than or equal to zero.")]
        [DisplayName("Open Stock (Quartz)")]
        public int OpenStockQ { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Case (Quartz)")]
        public decimal CostPerCaseQ { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Bottle (Quartz)")]
        public decimal CostPerBottleQ { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Sale Price (Quartz)")]
        public decimal SalePriceQ { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than or equal to zero.")]
        [DisplayName("Open Stock (Nib)")]
        public int OpenStockN { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Case (Nib)")]
        public decimal CostPerCaseN { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Bottle (Nib)")]
        public decimal CostPerBottleN { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Sale Price (Nib)")]
        public decimal SalePriceN { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than or equal to zero.")]
        [DisplayName("Open Stock (Pint)")]
        public int OpenStockP { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Case (Pint)")]
        public decimal CostPerCaseP { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Bottle (Pint)")]
        public decimal CostPerBottleP { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Sale Price (Pint)")]
        public decimal SalePriceP { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than or equal to zero.")]
        [DisplayName("Open Stock (Dip)")]
        public int OpenStockD { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Case (Dip)")]
        public decimal CostPerCaseD { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Bottle (Dip)")]
        public decimal CostPerBottleD { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Sale Price (Dip)")]
        public decimal SalePriceD { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be greater than or equal to zero.")]
        [DisplayName("Open Stock (Litre)")]
        public int OpenStockL { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Case (Litre)")]
        public decimal CostPerCaseL { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Cost Per Bottle (Litre)")]
        public decimal CostPerBottleL { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Value must be greater than or equal to  zero.")]
        [DisplayName("Sale Price (Litre)")]
        public decimal SalePriceL { get; set; }

    }
}

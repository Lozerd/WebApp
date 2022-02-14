using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{

    public class Product
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        [StringLength(255)]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }
        [DataType(DataType.MultilineText)]
        public string ProductDescription { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [DataType(DataType.Currency)]
        public float Price { get; set; }
        public bool IsSale { get; set; }
        [DataType(DataType.Currency)]
        public float DiscountValue { get; set; }
        [NotMapped]
        //public float SalePrice { get { return IsSale ? Price - DiscountValue : 0; } set => SalePrice = value; }
        public float SalePrice { get { return IsSale ? Price - DiscountValue : 0; } set => SalePrice = value; }
        [NotMapped]
        public float DiscountValuePercent { get { return DiscountValue / Price * 100; } set { DiscountValuePercent = value; } }
    }
}

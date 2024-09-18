using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Techify.Models
{
    public class ProductDetails
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and maximum 100 characters are allowed", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "500000", ErrorMessage = "Invalid Price")]
        public decimal Price { get; set; }

        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public int StockQuantity { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Invalid Category ID")]
        public int? CategoryID { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Invalid Brand ID")]
        public int? BrandID { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Invalid Seller ID")]
        public int? SellerID { get; set; }

        public string ImageURL { get; set; }
        public SelectList Categories { get; set; }
        public SelectList Brands { get; set; }
        public SelectList Sellers { get; set; }
    }

}
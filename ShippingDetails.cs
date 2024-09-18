using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techify.Models
{
    public class ShippingDetails
    {
        public int OrderID { get; set; }
        [Required]
        public System.DateTime OrderDate { get; set; }
        [Required]
        public Nullable<int> CustomerID { get; set; }
       
        public decimal TotalAmount { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string TrackingNumber { get; set; }
        [Required]
        public Nullable<System.DateTime> CreatedAt { get; set; }
        [Required]
        public Nullable<System.DateTime> UpdatedAt { get; set; }
    }
}
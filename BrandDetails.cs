using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techify.Models
{

    public class BrandDetails
    {
        public int BrandID { get; set; }
        [Required(ErrorMessage = "Brand Name Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 characters are allowed", MinimumLength = 3)]
        public string BrandName { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
    }
}   

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Techify.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SellerRequest
    {
        public int RequestID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string BranchName { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreatedAt { get; set; }
    }
}

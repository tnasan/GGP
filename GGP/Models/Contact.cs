//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GGP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Contact
    {
        public Contact()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    
        public virtual ICollection<Customer> Customers { get; set; }
    }
}

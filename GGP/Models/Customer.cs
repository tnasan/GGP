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
    
    public partial class Customer
    {
        public Customer()
        {
            this.Bills = new HashSet<Bill>();
            this.CustomerContacts = new HashSet<CustomerContact>();
            this.Inventories = new HashSet<Inventory>();
            this.ARPayments = new HashSet<ARPayment>();
        }
    
        public long Id { get; set; }
        public string Name { get; set; }
        public string TelephoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Email { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
    
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<CustomerContact> CustomerContacts { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<ARPayment> ARPayments { get; set; }
    }
}

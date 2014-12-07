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

    public partial class ARPayment
    {
        public ARPayment()
        {
            this.BillARPayments = new HashSet<BillARPayment>();
        }

        public long Id { get; set; }
        [Required]
        public int PaymentMethodId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [Required]
        public System.DateTime PaymentDate { get; set; }
        [Required]
        public long CompanyId { get; set; }
        [Required]
        public long CustomerId { get; set; }
    
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<BillARPayment> BillARPayments { get; set; }
        public virtual ARCheque ARCheque { get; set; }
        public virtual Company Company { get; set; }
        public virtual Customer Customer { get; set; }
    }
}

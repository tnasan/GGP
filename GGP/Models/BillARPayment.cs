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
    
    public partial class BillARPayment
    {
        public long BillID { get; set; }
        public long ARPaymentId { get; set; }
        public decimal Amount { get; set; }
    
        public virtual ARPayment ARPayment { get; set; }
        public virtual Bill Bill { get; set; }
    }
}

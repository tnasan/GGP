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
    
    public partial class UnifOfMeasurement
    {
        public UnifOfMeasurement()
        {
            this.Inventories = new HashSet<Inventory>();
        }
    
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}

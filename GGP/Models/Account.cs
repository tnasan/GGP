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
    using System.Web.Mvc;
    
    public partial class Account
    {
        public Account()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        [Required]
        [MaxLength(50)]
        [Remote("CheckUsernameExist", "Account", HttpMethod = "POST", ErrorMessage = "�ѭ�ռ������")]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
    }
}

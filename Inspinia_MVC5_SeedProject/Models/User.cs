using System.ComponentModel;

namespace Inspinia_MVC5_SeedProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        [DisplayName("UserName")]
        [StringLength(100)]
        public string username { get; set; }

        [Required]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [StringLength(32)]
        public string password { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [DisplayName("Address")]
        [StringLength(100)]
        public string address { get; set; }

        public int branch_id { get; set; }

        [DisplayName("Access All")]
        public bool isAccessAll { get; set; }

        public virtual Branch Branch { get; set; }
    }
}

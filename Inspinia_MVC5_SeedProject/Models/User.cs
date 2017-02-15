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
        [StringLength(100)]
        public string username { get; set; }

        [Required]
        [StringLength(32)]
        public string password { get; set; }

        [Required]
        [StringLength(100)]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string address { get; set; }

        public int branch_id { get; set; }

        public bool isAccessAll { get; set; }

        public virtual Branch Branch { get; set; }
    }
}

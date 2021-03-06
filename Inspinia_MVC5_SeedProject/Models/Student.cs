using System.ComponentModel;

namespace Inspinia_MVC5_SeedProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Enrollments = new HashSet<Enrollment>();
            Results = new HashSet<Result>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(32)]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }

        [Required]
        [DisplayName("Full Name")]
        [StringLength(255)]
        public string fullname { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Date Of Birth")]
//        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }

        [Required]
        [DisplayName("Address")]
        [StringLength(255)]
        public string address { get; set; }

        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string email { get; set; }

        public int branch_id { get; set; }

        public virtual Branch Branch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }
    }
}

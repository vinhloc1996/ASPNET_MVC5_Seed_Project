using System.ComponentModel;

namespace Inspinia_MVC5_SeedProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cours()
        {
            Enrollments = new HashSet<Enrollment>();
            Results = new HashSet<Result>();
            Schedules = new HashSet<Schedule>();
            Materials = new HashSet<Material>();

        }

        public int id { get; set; }
        [DisplayName("Course Name")]
        [Required]
        [StringLength(120)]
        public string name { get; set; }
        [DisplayName("Branch")]
        public int branch_id { get; set; }

        [Required]
        [DisplayName("Key for enrollment")]
        [StringLength(100)]
        public string key { get; set; }

        public virtual Branch Branch { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Result> Results { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Material> Materials { get; set; }
    }
}

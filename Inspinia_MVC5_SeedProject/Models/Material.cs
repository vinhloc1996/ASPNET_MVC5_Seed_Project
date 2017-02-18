using System.ComponentModel;

namespace Inspinia_MVC5_SeedProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Material
    {
        public int id { get; set; }

        public int course_id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [DisplayName("Description")]
        public string description { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Date Created")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime create_date { get; set; }

        public virtual Cours Cours { get; set; }
    }
}
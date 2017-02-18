namespace Inspinia_MVC5_SeedProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Schedule
    {
        public int id { get; set; }

        public int course_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_learn { get; set; }

        public virtual Cours Cours { get; set; }
    }
}

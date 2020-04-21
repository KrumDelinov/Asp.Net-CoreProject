namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Attendance : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(300)]
        public string Remark { get; set; }

        [Required]
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        [Required]
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}

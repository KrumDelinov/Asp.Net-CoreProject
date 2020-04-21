namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Classroom : BaseDeletableModel<int>
    {
        [Required]
        public int Number { get; set; }

        public string Description { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}

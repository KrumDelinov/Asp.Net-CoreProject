namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Classroom : BaseDeletableModel<int>
    {
        public string NumberDescription { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
    }
}

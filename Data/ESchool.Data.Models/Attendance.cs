namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Attendance : BaseDeletableModel<int>
    {
        public string Remark { get; set; }

        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}

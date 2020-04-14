namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Exam : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public decimal Result { get; set; }

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual ICollection<StudentsExams> StudentsExam { get; set; } = new HashSet<StudentsExams>();
    }
}

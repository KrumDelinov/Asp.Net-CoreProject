namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Classroom : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public DateTime Year { get; set; }

        public int StudentCount { get; set; } = 25;

        public int TeacherId { get; set; }

        public virtual Teacher MainTeacher { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        public ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();
    }
}

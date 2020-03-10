namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Subject : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; } = new HashSet<Teacher>();

        public virtual ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();
    }
}

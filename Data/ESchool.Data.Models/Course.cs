namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public int Issue { get; set; }

        public string Description { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

        public virtual ICollection<Subject> Subjects { get; set; } = new HashSet<Subject>();

        public virtual ICollection<CoursesTeachers> CourseTeachers { get; set; } = new HashSet<CoursesTeachers>();
    }
}

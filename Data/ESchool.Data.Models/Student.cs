namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Student : BaseDeletableModel<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int GradeId { get; set; }

        public virtual Grade Grade { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();

        public virtual ICollection<Exam> Exams { get; set; } = new HashSet<Exam>();

        public ICollection<ParentStudent> StudentParents { get; set; } = new HashSet<ParentStudent>();
    }
}

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
        [MinLength(3)]
        [MaxLength(10)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();

        public virtual ICollection<StudentsExams> StudentExams { get; set; } = new HashSet<StudentsExams>();

        public virtual ICollection<ParentStudent> StudentParents { get; set; } = new HashSet<ParentStudent>();
    }
}

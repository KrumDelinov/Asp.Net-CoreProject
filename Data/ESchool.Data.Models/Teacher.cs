namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Teacher : BaseDeletableModel<int>
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

        public bool HasClassroom { get; set; } = false;

        public bool HasGrade { get; set; } = false;

        public string UserId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CoursesTeachers> CoursesTeacher { get; set; } = new HashSet<CoursesTeachers>();
    }
}

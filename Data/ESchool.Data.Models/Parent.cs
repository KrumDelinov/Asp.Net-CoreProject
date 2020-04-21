namespace ESchool.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Common.Models;

    public class Parent : BaseDeletableModel<int>
    {
        [Required]
        [StringLength(10)]
        [MinLength(3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(10)]
        [MinLength(3)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<ParentStudent> ParentStudents { get; set; } = new HashSet<ParentStudent>();
    }
}

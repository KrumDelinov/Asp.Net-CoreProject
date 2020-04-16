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
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ICollection<ParentStudent> ParentStudents { get; set; } = new HashSet<ParentStudent>();
    }
}

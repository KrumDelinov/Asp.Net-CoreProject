namespace ESchool.Web.ViewModels.Teachers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class TeacherCreateInputModel : IMapFrom<Teacher>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name ="Subject")]
        public int SubjectId { get; set; }

        public IEnumerable<SubjectsDropDownViewModel> Subjects { get; set; }
    }
}

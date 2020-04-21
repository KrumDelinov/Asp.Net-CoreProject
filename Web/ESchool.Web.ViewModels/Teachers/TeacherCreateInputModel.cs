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
        [Required(ErrorMessage = "Името е задължително")]
        [Display(Name = "Име")]
        [StringLength(10, ErrorMessage = "{0} имети трябва да е между {2} и {1}.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(10, ErrorMessage = "{0} имети трябва да е между {2} и {1}.", MinimumLength = 3)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name ="Предмет")]
        public int SubjectId { get; set; }

        public IEnumerable<SubjectsDropDownViewModel> Subjects { get; set; }
    }
}

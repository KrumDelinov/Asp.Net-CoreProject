using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESchool.Web.ViewModels.Students
{
    public class StudentsCreateViewModel : IMapFrom<Student>
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
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата на раждане")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Паралелка")]
        public int CourseId { get; set; }

        public IEnumerable<CourseDropDownViewModel> Courses { get; set; }
    }
}

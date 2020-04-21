namespace ESchool.Web.ViewModels.Grades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Classrooms;

    public class CourseCreateInputViewModel : IMapFrom<Course>
    {


        [Required]
        [Range(2000, 2100, ErrorMessage = "{0} Годината трябва да по-голяма от {1}.")]
        [Display(Name = "Набор")]
        public int Issue { get; set; }

        [Required]
        [Display(Name = "Паралелка. Клас от 1 до 12. Паралелка от A до H")]
        [RegularExpression("^([1-9]|[1][0-2])[A-H]{1}$", ErrorMessage = "Невалиден формат")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Учител")]
        public int TeacherId { get; set; }

        public IEnumerable<TeacherDropDownViewModel> Teachers { get; set; }

        public int CurrentYear => int.Parse(DateTime.UtcNow.Year.ToString());

        public string CurrentCourse => (this.CurrentYear - this.Issue).ToString();
    }
}

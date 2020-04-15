namespace ESchool.Web.ViewModels.Grades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Classrooms;

    public class CourseEditViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Набор")]
        public int Issue { get; set; }

        [Required]
        [Display(Name = "Паралелка")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Teachers")]
        public int TeacherId { get; set; }

        public IEnumerable<TeacherDropDownViewModel> Teachers { get; set; }
    }
}

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
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [Display(Name = "Grades")]
        public int GradeId { get; set; }

        public IEnumerable<GradeDropDownViewModel> Grades { get; set; }
    }
}

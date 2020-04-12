using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Students
{
    public class StudentViewModel : IMapFrom<Student>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string GradeDescription { get; set; }
    }
}

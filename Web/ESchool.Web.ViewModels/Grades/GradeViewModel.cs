namespace ESchool.Web.ViewModels.Grades
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Students;

    public class GradeViewModel : IMapFrom<Grade>
    {
        public int Id { get; set; }

        public int Issue { get; set; }

        public string Description { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherSubjectDescription { get; set; }

        public IEnumerable<StudentViewModel> Students { get; set; }
    }
}

namespace ESchool.Web.ViewModels.Grades
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Classrooms;

    public class CourseTeachersViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public int Issue { get; set; }

        public string Description { get; set; }

        public int TeacherId { get; set; }

        public string TeacherFirstName { get; set; }

        public IEnumerable<TeacherDropDownViewModel> Teachers { get; set; }
    }
}

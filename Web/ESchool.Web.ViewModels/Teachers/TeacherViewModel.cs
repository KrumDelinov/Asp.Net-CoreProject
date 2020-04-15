namespace ESchool.Web.ViewModels.Teachers
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Grades;
    using System.Collections.Generic;

    public class TeacherViewModel : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SubjectDescription { get; set; }

        public string UserUserName { get; set; }

        public virtual IEnumerable<CourseViewModel> TeacherCourses { get; set; }
    }
}

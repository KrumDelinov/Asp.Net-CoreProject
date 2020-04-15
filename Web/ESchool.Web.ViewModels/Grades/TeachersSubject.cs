using ESchool.Data.Models;
using ESchool.Services.Mapping;

namespace ESchool.Web.ViewModels.Grades
{
    public class TeachersSubject : IMapFrom<CoursesTeachers>
    {

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherSubjectDescription { get; set; }

    }
}
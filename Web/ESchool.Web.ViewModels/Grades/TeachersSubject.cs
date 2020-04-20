using ESchool.Data.Models;
using ESchool.Services.Mapping;

namespace ESchool.Web.ViewModels.Grades
{
    public class TeachersSubject : IMapFrom<CoursesTeachers>
    {
        public int TeacherId { get; set; }

        public int CourseId { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherSubjectDescription { get; set; }
    }
}
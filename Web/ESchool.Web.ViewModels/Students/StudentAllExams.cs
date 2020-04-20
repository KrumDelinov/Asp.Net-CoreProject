using ESchool.Data.Models;
using ESchool.Services.Mapping;

namespace ESchool.Web.ViewModels.Students
{
    public class StudentAllExams : IMapFrom<Exam>
    {

        public string ExamType { get; set; }

        public string CreatedOn { get; set; }

        public decimal Result { get; set; }

        public int ExamId { get; set; }

        public int TeacherId { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherFullName => $"{this.TeacherFirstName} {this.TeacherLastName}";

        public string TeacherSubjectDescription { get; set; }

        public int StudentExamsCount { get; set; }
    }
}
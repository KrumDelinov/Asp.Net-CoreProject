namespace ESchool.Web.ViewModels.Students
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class StudentAttendances : IMapFrom<Attendance>
    {
        public string Remark { get; set; }

        public int StudentId { get; set; }

        public string CreatedOn { get; set; }

        public int TeacherId { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public string TeacherFullName => $"{this.TeacherFirstName} {this.TeacherLastName}";
    }
}
namespace ESchool.Web.ViewModels.Students
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class StudentForParentViewModel : IMapFrom<Student>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public virtual IEnumerable<StudentAllExams> StudentExams { get; set; }

        public virtual IEnumerable<StudentAttendances> Attendances { get; set; }
    }
}

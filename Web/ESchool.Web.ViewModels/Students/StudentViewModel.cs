namespace ESchool.Web.ViewModels.Students
{
    using System;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class StudentViewModel : IMapFrom<Student>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string CourseDescription { get; set; }
    }
}

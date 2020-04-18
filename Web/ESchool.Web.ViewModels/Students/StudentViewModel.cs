namespace ESchool.Web.ViewModels.Students
{
    using System;

    using AutoMapper;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class StudentViewModel : IMapFrom<Student>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int CourseId { get; set; }

        public string CourseDescription { get; set; }

        public string FullNameAndBirthDate => $"{this.FirstName} {this.LastName} {this.BirthDate:dd/mm/yyyy/}";
    }
}

namespace ESchool.Web.ViewModels.Students
{
    using System;

    using AutoMapper;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class StudentViewModel : IMapFrom<Student>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime BirthDate { get; set; }

        public int CourseId { get; set; }

        public string CourseDescription { get; set; }

        public string FullNameAndBirthDate { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Student, StudentViewModel>().ForMember(
              m => m.FullNameAndBirthDate,
              opt => opt.MapFrom(x => $"{x.BirthDate:yyyy/MM/dd}"));
        }
    }
}

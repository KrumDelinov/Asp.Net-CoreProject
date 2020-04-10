namespace ESchool.Web.ViewModels.Classrooms
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class TeacherDropDownViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string SubjectDescription { get; set; }

        public bool HasClassroom { get; set; }

        public bool HasGrade { get; set; }

        public string FullNameAndSubject { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Teacher, TeacherDropDownViewModel>().ForMember(
              m => m.FullNameAndSubject,
              opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName} ({x.Subject.Description})"));
        }
    }
}

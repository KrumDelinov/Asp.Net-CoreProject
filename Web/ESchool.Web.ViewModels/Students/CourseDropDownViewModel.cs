using AutoMapper;
using ESchool.Data.Models;
using ESchool.Services.Mapping;

namespace ESchool.Web.ViewModels.Students
{
    public class CourseDropDownViewModel : IMapFrom<Course>, IHaveCustomMappings
    {

        public int Id { get; set; }

        public string Description { get; set; }

        public string TeacherFullName { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Teacher, CourseDropDownViewModel>().ForMember(
             m => m.TeacherFullName,
             opt => opt.MapFrom(x => $" ({x.FirstName} {x.LastName}) "));
        }
    }
}
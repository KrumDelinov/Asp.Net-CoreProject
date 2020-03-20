namespace ESchool.Web.ViewModels.Home
{
    using AutoMapper;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class IndexGradeViewModel : IMapFrom<Grade>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string FullName { get; set; }

        public int StudentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Grade, IndexGradeViewModel>().ForMember(
                m => m.FullName,
                opt => opt.MapFrom(x => x.Teacher.FirstName + "  " + x.Teacher.LastName));
        }
    }
}

namespace ESchool.Web.ViewModels.Home
{
    using AutoMapper;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class IndexClassroomViewModel : IMapFrom<Classroom>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public string FullName { get; set; }

        public int StudentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Classroom, IndexClassroomViewModel>().ForMember(
                m => m.FullName,
                opt => opt.MapFrom(x => x.MainTeacher.FirstName + "  " + x.MainTeacher.LastName));
        }
    }
}

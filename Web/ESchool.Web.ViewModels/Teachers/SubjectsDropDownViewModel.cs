namespace ESchool.Web.ViewModels.Teachers
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SubjectsDropDownViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}

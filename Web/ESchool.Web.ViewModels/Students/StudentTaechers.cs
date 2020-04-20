namespace ESchool.Web.ViewModels.Students
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Grades;

    public class StudentTaechers : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string SubjectDescription { get; set; }

    }
}
namespace ESchool.Web.ViewModels.Teachers
{
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class TeacherViewModel : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SubjectDescription { get; set; }

        public string UserUserName { get; set; }
    }
}

namespace ESchool.Web.ViewModels.Parents
{

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class ParentViewModel : IMapFrom<Parent>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }


    }
}

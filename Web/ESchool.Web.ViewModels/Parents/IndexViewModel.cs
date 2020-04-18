namespace ESchool.Web.ViewModels.Parents
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Students;

    public class IndexViewModel : IMapFrom<Parent>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserUserName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";

        public virtual IEnumerable<StudentViewModel> ParentStudents { get; set; }
    }
}

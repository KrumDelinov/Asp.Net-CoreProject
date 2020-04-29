namespace ESchool.Web.ViewModels.Administration.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class RoleViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }

    }
}

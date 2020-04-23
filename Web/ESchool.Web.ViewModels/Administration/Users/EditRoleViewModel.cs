using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Administration.Users
{
    public class EditRoleViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserViewModel> RoleUsers { get; set; }
    }
}

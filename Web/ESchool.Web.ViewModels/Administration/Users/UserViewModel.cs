namespace ESchool.Web.ViewModels.Administration.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsSelected { get; set; }

        public virtual ICollection<RoleViewModel> Roles { get; set; }
    }
}

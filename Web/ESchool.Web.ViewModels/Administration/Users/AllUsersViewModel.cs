namespace ESchool.Web.ViewModels.Administration.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllUsersViewModel
    {
        public virtual ICollection<UserViewModel> AllUsers { get; set; }

        public ICollection<EditRoleViewModel> Roles { get; set; }
    }
}

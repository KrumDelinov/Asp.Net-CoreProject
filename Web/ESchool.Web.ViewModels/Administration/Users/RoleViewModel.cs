using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Administration.Users
{
    public class RoleViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }

    }
}

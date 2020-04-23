namespace ESchool.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Common.Models;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult AllRoles()
        {
            var roles = this.roleManager.Roles;

            return this.View(roles);
        }

        public async Task<IActionResult> UserDetails(string userId)
        {
            //userId = "276fa1ca-c1d8-4b68-a506-b462758b56ba";
            var user = await this.userManager.FindByIdAsync(userId);

            var roles = await this.userManager.GetRolesAsync(user);

            //var roles = this.userManager.Users.Where(x => x.Id == userId).Select(x => x.Roles).ToList();

            var userRoles = new List<RoleViewModel>();

            foreach (var name in roles)
            {
                var roleModel = new RoleViewModel
                {
                    Name = name,
                };

                userRoles.Add(roleModel);
            }

            var viewModel = new UserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Roles = userRoles,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> AllUsersAsync()
        {
            var viewModel = new AllUsersViewModel();

            var allRoles = this.roleManager.Roles;

            var listRoles = new List<EditRoleViewModel>();

            foreach (var role in allRoles)
            {
                var editRoleModel = new EditRoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                };

                listRoles.Add(editRoleModel);
            }

            var users = new List<UserViewModel>();


            foreach (var item in this.userManager.Users)
            {
                var roles = await this.userManager.GetRolesAsync(item);

                var userRoles = new List<RoleViewModel>();

                foreach (var name in roles)
                {
                    var roleModel = new RoleViewModel
                    {
                        Name = name,
                    };

                    userRoles.Add(roleModel);
                }

                var user = new UserViewModel
                {
                    UserId = item.Id,
                    UserName = item.UserName,
                    CreatedOn = item.CreatedOn,
                    Roles = userRoles,

                };

                users.Add(user);
            }

            viewModel.AllUsers = users;
            viewModel.Roles = listRoles;
            return this.View(viewModel);
        }

        
        public async Task<IActionResult> EditUserRoles(string userId, string roleId)
        {

            return null;
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await this.roleManager.FindByIdAsync(id);

            var users = new List<UserViewModel>();

            foreach (var user in this.userManager.Users)
            {

                var userRoleViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    CreatedOn = user.CreatedOn,
                };

                users.Add(userRoleViewModel);
            }

            var viewModel = new EditRoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                RoleUsers = users,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await this.roleManager.FindByIdAsync(model.Id);

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            foreach (var user in model.RoleUsers)
            {
                var currentUser = await this.userManager.FindByIdAsync(user.UserId);
                bool isInRole = await this.userManager.IsInRoleAsync(currentUser, model.Name);

                IdentityResult result = null;

                if (user.IsSelected && !isInRole)
                {
                    result = await this.userManager.AddToRoleAsync(currentUser, model.Name);
                }
                else if (!user.IsSelected && isInRole)
                {
                    result = await this.userManager.RemoveFromRoleAsync(currentUser, model.Name);
                }
                else
                {
                    continue;
                }
            }

            return this.RedirectToAction("AllRoles");
        }

     
    }
}
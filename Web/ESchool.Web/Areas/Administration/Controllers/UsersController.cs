namespace ESchool.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public UsersController
            (
                UserManager<ApplicationUser> userManager,
                RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult AllRoles()
        {
            var roles = this.roleManager.Roles;

            return this.View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserRoles(string id)
        {

            var user = await this.userManager.FindByIdAsync(id);

            var roles = await this.userManager.GetRolesAsync(user);

            var allRoles = this.roleManager.Roles;

            var userRoles = new List<RoleViewModel>();

            foreach (var role in allRoles)
            {

                var roleModel = new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                };


                userRoles.Add(roleModel);
            }

            var viewModel = new UserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                CreatedOn = user.CreatedOn,
                Roles = userRoles,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult EditUserRoles(UserViewModel model, string id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.View(this.NotFound());

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

        public async Task<IActionResult> UserDetailsAsync(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roles = await this.userManager.GetRolesAsync(user);
            var allRoles = this.roleManager.Roles;
            var userRoles = new List<RolesDropDownViewModel>();

            foreach (var role in allRoles)
            {
                bool isInRole = roles.Contains(role.Name);
                if (isInRole)
                {
                    continue;
                }
                else
                {
                    var roleModel = new RolesDropDownViewModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                    };
                    userRoles.Add(roleModel);
                }

            }

            var viewModel = new EditUserRolesViewModel
            {
                UserName = user.UserName,
                UserId = user.Id,
                Roles = userRoles,

            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserDetailsAsync(EditUserRolesViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var roles = await this.userManager.GetRolesAsync(user);
            var allRoles = this.roleManager.Roles;

            var userRoles = new List<RolesDropDownViewModel>();

            foreach (var role in allRoles)
            {
                var roleModel = new RolesDropDownViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                };
                userRoles.Add(roleModel);
            }

            var viewModel = new EditUserRolesViewModel
            {
                UserName = user.UserName,
                UserId = user.Id,
                Roles = userRoles,

            };

            var roleName = allRoles
           .Where(n => n.Id == model.RoleId)
           .Select(x => x.Name)
           .FirstOrDefault();

            var result = await this.userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return this.RedirectToAction("AllUsers");
            }
            else
            {
                return this.View(viewModel);
            }
        }

        public async Task<IActionResult> RemoveUserFromRole(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            var roles = await this.userManager.GetRolesAsync(user);
            var allRoles = this.roleManager.Roles;
            var userRoles = new List<RolesDropDownViewModel>();

            foreach (var role in allRoles)
            {
                bool isInRole = roles.Contains(role.Name);

                if (!isInRole)
                {
                    continue;
                }
                else
                {
                    var roleModel = new RolesDropDownViewModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                    };
                    userRoles.Add(roleModel);
                }

            }

            var viewModel = new EditUserRolesViewModel
            {
                UserName = user.UserName,
                UserId = user.Id,
                Roles = userRoles,

            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(EditUserRolesViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.UserId);
            var roles = await this.userManager.GetRolesAsync(user);
            var allRoles = this.roleManager.Roles;

            var userRoles = new List<RolesDropDownViewModel>();

            foreach (var role in allRoles)
            {
                bool isInRole = roles.Contains(role.Name);

                if (!isInRole)
                {
                    continue;
                }
                else
                {
                    var roleModel = new RolesDropDownViewModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                    };
                    userRoles.Add(roleModel);
                }

            }

            var viewModel = new EditUserRolesViewModel
            {
                UserName = user.UserName,
                UserId = user.Id,
                Roles = userRoles,

            };

            var roleName = allRoles
           .Where(n => n.Id == model.RoleId)
           .Select(x => x.Name)
           .FirstOrDefault();

            var result = await this.userManager.RemoveFromRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return this.RedirectToAction("AllUsers");
            }
            else
            {
                return this.View(viewModel);
            }
        }

    }
}
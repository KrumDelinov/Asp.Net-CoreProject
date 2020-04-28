namespace ESchool.Web.Areas.Teachers.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.Areas.Teacher;
    using ESchool.Web.ViewModels.Parents;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ParentsController : TeachersController
    {
        private readonly IParentServices parentServices;
        private readonly IStudentsServices studentsServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public ParentsController(
            IParentServices parentServices,
            IStudentsServices studentsServices,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.parentServices = parentServices;
            this.studentsServices = studentsServices;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Create(int id)
        {
            var viewModel = new PerentCreateViewModel
            {
                StudentId = id,
            };

            var hasParent = this.parentServices.HasParent(viewModel.Email);

            if (hasParent)
            {
                return this.RedirectToAction("Edit", new { id = viewModel.Id });
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(PerentCreateViewModel input)
        {
            var hasParent = this.parentServices.HasParent(input.Email);

            var hasStudent = this.parentServices.ParentHasStudent(input.Email, input.StudentId);

            var parentUser = await this.userManager.FindByEmailAsync(input.Email);

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            else if (parentUser == null && !hasParent)
            {
                var user = new ApplicationUser
                {
                    UserName = input.Email,
                    Email = input.Email,
                    EmailConfirmed = true,
                };

                var password = "123456";

                var result = await this.userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    int parentId = await this.CrateParent(input, user);
                    await this.userManager.AddToRoleAsync(user, GlobalConstants.ParentRoleName);
                    return this.RedirectToAction("Details", new { id = parentId });
                }
            }
            else if (parentUser != null && !hasParent)
            {
                var isInRole = this.userManager.IsInRoleAsync(parentUser, GlobalConstants.ParentRoleName);

                if (await isInRole)
                {
                    int parentId = await this.CrateParent(input, parentUser);
                    return this.RedirectToAction("Details", new { id = parentId });
                }
                else
                {
                    int parentId = await this.CrateParent(input, parentUser);
                    await this.userManager.AddToRoleAsync(parentUser, GlobalConstants.ParentRoleName);
                    return this.RedirectToAction("Details", new { id = parentId });
                }
            }
            else if (parentUser != null && hasParent && !hasStudent)
            {
                var parentId = this.parentServices.GetParentId(input.Email);
                await this.parentServices.SetStudentToParent(parentId, input.StudentId);
                return this.RedirectToAction("Details", new { id = parentId });
            }

            return this.View("ParentHasStudent");
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.parentServices.Parent<ParentViewModel>(id);

            return this.View(viewModel);
        }

        private async Task<int> CrateParent(PerentCreateViewModel input, ApplicationUser user)
        {
            var parentId = await this.parentServices.CreateAsync(input.FirstName, input.LastName, input.Email, user.Id);

            await this.parentServices.SetStudentToParent(parentId, input.StudentId);
            return parentId;
        }
    }
}

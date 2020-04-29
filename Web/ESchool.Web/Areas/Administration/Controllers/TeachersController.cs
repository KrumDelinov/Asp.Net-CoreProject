namespace ESchool.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Web.ViewModels.Teachers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    public class TeachersController : AdministrationController
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITeacherServises teacherServises;
        private readonly ISubjectsServices subjectsServices;
        private readonly RoleManager<ApplicationRole> roleManager;

        public TeachersController(
            UserManager<ApplicationUser> userManager,
            ITeacherServises teacherServises,
            ISubjectsServices subjectsServices,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.teacherServises = teacherServises;
            this.subjectsServices = subjectsServices;
            this.roleManager = roleManager;
        }


        public IActionResult Create()
        {
            var subjects = this.subjectsServices.GetAll<SubjectsDropDownViewModel>();
            var viewModel = new TeacherCreateInputModel();
            viewModel.Subjects = subjects;
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(TeacherCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Subjects = this.subjectsServices.GetAll<SubjectsDropDownViewModel>();
                return this.View(inputModel);
            }

            var teacherId = await this.teacherServises.CreateAsync(inputModel.FirstName, inputModel.LastName, inputModel.SubjectId, inputModel.Email);

            var findUser = await this.userManager.FindByEmailAsync(inputModel.Email);

            if (findUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = inputModel.Email,
                    Email = inputModel.Email,
                    EmailConfirmed = true,
                };

                var password = "123456";

                var result = await this.userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await this.teacherServises.AddUserToTeacher(teacherId, user.Id);
                    await this.userManager.AddToRoleAsync(user, GlobalConstants.TeacherRoleName);
                    return this.RedirectToAction("Details", new { id = teacherId });
                }
            }
            else
            {
                var result = await this.userManager.AddToRoleAsync(findUser, GlobalConstants.TeacherRoleName);
                await this.teacherServises.AddUserToTeacher(teacherId, findUser.Id);
                if (result.Succeeded)
                {
                    return this.RedirectToAction("Details", new { id = teacherId });
                }
            }

            return this.RedirectToAction("Details", new { id = teacherId });
        }

        public IActionResult All()
        {
            var teachers = this.teacherServises.GetAll<TeacherViewModel>();
            var viewModel = new AllTeachersViewModel
            {
                Teachers = teachers,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            TeacherViewModel viewModel = this.teacherServises.Teacher<TeacherViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var teacher = this.teacherServises.GetTeacher(id);
            var subjects = this.subjectsServices.GetAll<SubjectsDropDownViewModel>();

            string empty = "No Email";
            if (teacher.Email == null)
            {
                teacher.Email = empty;
            }

            var viewModel = new TeacherEditViewModel
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                SubjectId = teacher.SubjectId,
                Subjects = subjects,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeacherEditViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var teacher = this.teacherServises.GetTeacher(viewModel.Id);
            string empty = "No Email";
            if (viewModel.Email == null)
            {
                viewModel.Email = empty;
            }

            teacher.FirstName = viewModel.FirstName;
            teacher.LastName = viewModel.LastName;
            teacher.Email = viewModel.Email.ToUpper();
            teacher.SubjectId = viewModel.SubjectId;

            await this.teacherServises.UpdateTeacher(teacher);

            return this.RedirectToAction("Details", new { id = viewModel.Id });
        }

        public IActionResult Delete(int id)
        {
            TeacherViewModel viewModel = this.teacherServises.Teacher<TeacherViewModel>(id);

            if (!this.ModelState.IsValid )
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = this.teacherServises.GetTeacher(id);

            await this.teacherServises.DeleteTeacher(teacher);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}

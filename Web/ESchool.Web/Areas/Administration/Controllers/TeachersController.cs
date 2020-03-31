namespace ESchool.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Web.ViewModels.Teachers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        public IActionResult Create()
        {
            var subjects = this.subjectsServices.GetAll<SubjectsDropDownViewModel>();
            var viewModel = new TeacherCreateInputModel();
            viewModel.Subjects = subjects;
            return this.View(viewModel);
        }

        // POST: Taechers/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(TeacherCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            //var user = await this.userManager.GetUserAsync(this.User);
            
            //var role = roleManager.GetRoleNameAsync()
            var teacherId = await this.teacherServises.CreateAsync(inputModel.FirstName, inputModel.LastName, inputModel.SubjectId);

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
    }
}

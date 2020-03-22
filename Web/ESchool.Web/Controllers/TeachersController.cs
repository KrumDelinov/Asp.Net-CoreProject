namespace ESchool.Web.Controllers
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

    public class TeachersController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITeacherServises teacherServises;
        private readonly ISubjectsServices subjectsServices;

        public TeachersController(
            UserManager<ApplicationUser> userManager,
            ITeacherServises teacherServises,
            ISubjectsServices subjectsServices)
        {
            this.userManager = userManager;
            this.teacherServises = teacherServises;
            this.subjectsServices = subjectsServices;
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

            var user = await this.userManager.GetUserAsync(this.User);
            var teacherId = await this.teacherServises.CreateAsync(inputModel.FirstName, inputModel.LastName, user.Id, inputModel.SubjectId);

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

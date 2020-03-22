using ESchool.Services.Data;
using ESchool.Web.ViewModels.Classrooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESchool.Web.Controllers
{
    public class ClassroomsController : BaseController
    {
        private readonly IClassroomsServices classroomsServices;
        private readonly ITeacherServises teacherServises;

        public ClassroomsController(IClassroomsServices classroomsServices, ITeacherServises teacherServises)
        {
            this.classroomsServices = classroomsServices;
            this.teacherServises = teacherServises;
        }

        public IActionResult Create()
        {
            var techers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new ClassroomCrateInputViewModel();
            viewModel.Teachers = techers;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(ClassroomCrateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var classroomId = await this.classroomsServices.CreateAsync(input.NumberDescription, input.TeacherId);

            await this.teacherServises.SetClassroomToTeacher(input.TeacherId);

            return this.RedirectToAction("All");
        }
    }
}

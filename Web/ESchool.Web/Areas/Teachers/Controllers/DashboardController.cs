namespace ESchool.Web.Areas.Teacher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Grades;
    using ESchool.Web.ViewModels.Teachers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : TeachersController
    {
        private readonly ICoursesServices coursesServices;
        private readonly ITeacherServises teacherServises;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public DashboardController(
            ICoursesServices coursesServices,
            ITeacherServises teacherServises,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.coursesServices = coursesServices;
            this.teacherServises = teacherServises;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            string userId = await this.GetUserId();

            var teacher = this.teacherServises.GetUserTeacher(userId);

            var viewModel = this.teacherServises.UserTeacher<IndexTeacherViewModel>(userId);

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            string userId = await this.GetUserId();

            var techer = this.teacherServises.GetUserTeacher(userId);

            var courses = this.coursesServices.GetAllTeacherCourses<CourseViewModel>(techer.Id);

            var viewModel = new TeacherViewModel
            {
                TeacherCourses = courses,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> MyCourse()
        {
            string userId = await this.GetUserId();

            var techer = this.teacherServises.GetUserTeacher(userId);

            var courseId = this.coursesServices.GetTeacherCourseId(techer.Id);

            CourseViewModel viewModel = this.coursesServices.Course<CourseViewModel>(courseId);

            if (viewModel == null)
            {
                return this.View("NotFound");
            }

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            CourseViewModel viewModel = this.coursesServices.Course<CourseViewModel>(id);

            return this.View(viewModel);
        }

        private async Task<string> GetUserId()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);
            return userId;
        }
    }
}
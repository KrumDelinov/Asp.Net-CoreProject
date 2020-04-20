namespace ESchool.Web.Areas.Students.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Grades;
    using ESchool.Web.ViewModels.Students;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : StudentsController
    {
        private readonly IParentServices parentServices;
        private readonly IStudentsServices studentsServices;
        private readonly IExamsServices examsServices;
        private readonly IAtendacesServices atendacesServices;
        private readonly ICoursesServices coursesServices;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            IParentServices parentServices,
            IStudentsServices studentsServices,
            IExamsServices examsServices,
            IAtendacesServices atendacesServices,
            ICoursesServices coursesServices,
            UserManager<ApplicationUser> userManager)
        {
            this.parentServices = parentServices;
            this.studentsServices = studentsServices;
            this.examsServices = examsServices;
            this.atendacesServices = atendacesServices;
            this.coursesServices = coursesServices;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string userId = await this.GetUserId();

            var student = this.studentsServices.GetUserStudent(userId);

            var subjects = this.coursesServices.GetCourseAllTeachers<StudentTaechers>(student.CourseId);

            var exams = this.examsServices.GetAllStuentExans<StudentAllExams>(student.Id);

            var viewModel = new IndexStudentViewModel
            {
                AllExams = exams,
                StudentTeachers = subjects,

            };

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
namespace ESchool.Web.Areas.Parents.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Parents;
    using ESchool.Web.ViewModels.Students;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class DashboardController : ParentsController
    {
        private readonly IParentServices parentServices;
        private readonly IStudentsServices studentsServices;
        private readonly IExamsServices examsServices;
        private readonly IAtendacesServices atendacesServices;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            IParentServices parentServices,
            IStudentsServices studentsServices,
            IExamsServices examsServices,
            IAtendacesServices atendacesServices,
            UserManager<ApplicationUser> userManager)
        {
            this.parentServices = parentServices;
            this.studentsServices = studentsServices;
            this.examsServices = examsServices;
            this.atendacesServices = atendacesServices;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string userId = await this.GetUserId();

            var parent = this.parentServices.GetUserParent(userId);

            var students = this.studentsServices.GetAllParentStudents<StudentViewModel>(parent.Id);

            if (students == null)
            {
                return this.View("NotFound");
            }

            var viewModel = new IndexViewModel
            {
                FirstName = $"{parent.FirstName} {parent.LastName}",
                UserUserName = parent.Email,
                ParentStudents = students,
            };

            return this.View(viewModel);
        }

        public IActionResult Exams(int id)
        {
            var exams = this.examsServices.GetAllStuentExans<StudentAllExams>(id);
            var attendances = this.studentsServices.GetAllStudentAttendaces<StudentAttendances>(id);

            var hasExams = exams.Count();
            if (hasExams == 0)
            {
                return this.View("NotFoundStudent");

            }

            var viewModel = new StudentAllRolesViewModel
            {
                Attendances = attendances,
                StudentExams = exams,
            };

            return this.View(viewModel);
        }

        public IActionResult Attendances(int id)
        {
            var attendances = this.studentsServices.GetAllStudentAttendaces<StudentAttendances>(id);

            var viewModel = new StudentAllRolesViewModel
            {
                Attendances = attendances,
            };

            return this.View(viewModel);
        }

        private async Task<string> GetUserId()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);
            return userId;
        }

        private StudentAllRolesViewModel StudentViewModel(int id)
        {
            var exams = this.examsServices.GetAllStuentExans<StudentAllExams>(id);
            var attendances = this.studentsServices.GetAllStudentAttendaces<StudentAttendances>(id);
            var viewModel = new StudentAllRolesViewModel
            {
                Attendances = attendances,
                StudentExams = exams,
            };
            return viewModel;
        }
    }
}

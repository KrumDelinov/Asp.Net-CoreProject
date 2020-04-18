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
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

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

            var viewModel = new IndexViewModel
            {
                ParentStudents = students,
            };

            return this.View(viewModel);
        }

        public IActionResult Exams(int id)
        {

            var exams = this.examsServices.GetAllStuentExans<StudentAllExams>(id);

            var viewModel = new StudentForParentViewModel
            {
                StudentExams = exams,
            };

            return this.View(viewModel);
        }

        public IActionResult Attendances(int id)
        {

            var attendances = this.studentsServices.GetAllStudentAttendaces<StudentAttendances>(id);

            var viewModel = new StudentForParentViewModel
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

    }
}

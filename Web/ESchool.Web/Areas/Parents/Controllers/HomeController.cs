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

    public class HomeController : ParentsController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IParentServices parentServices;
        private readonly IStudentsServices studentsServices;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            IParentServices parentServices,
            IStudentsServices studentsServices)
        {
            this.userManager = userManager;
            this.parentServices = parentServices;
            this.studentsServices = studentsServices;
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

        private async Task<string> GetUserId()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);
            return userId;
        }
    }
}
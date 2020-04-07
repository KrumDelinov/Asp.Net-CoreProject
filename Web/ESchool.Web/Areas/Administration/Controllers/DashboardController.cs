namespace ESchool.Web.Areas.Administration.Controllers
{
    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Web.ViewModels.Administration.Dashboard;
    using ESchool.Web.ViewModels.Teachers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ITeacherServises teacherServises;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public DashboardController(ISettingsService settingsService,
            ITeacherServises teacherServises,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.settingsService = settingsService;
            this.teacherServises = teacherServises;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            //var teachers = this.teacherServises.GetAll<TeacherViewModel>();
            //var viewModel = new AllTeachersViewModel
            //{
            //    Teachers = teachers,
            //};

            //return this.View(viewModel);
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}

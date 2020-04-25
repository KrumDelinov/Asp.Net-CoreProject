namespace ESchool.Web.Areas.Administration.Controllers
{
    using ESchool.Common;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Administration.Dashboard;
    using ESchool.Web.ViewModels.Teachers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;
        private readonly ITeacherServises teacherServises;
        private readonly ICoursesServices coursesServices;
        private readonly IParentServices parentServices;
        private readonly IStudentsServices studentsServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public DashboardController(
            ITeacherServises teacherServises,
            ICoursesServices coursesServices,
            IParentServices parentServices,
            IStudentsServices studentsServices,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            this.teacherServises = teacherServises;
            this.coursesServices = coursesServices;
            this.parentServices = parentServices;
            this.studentsServices = studentsServices;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {

            var viewModel = new IndexViewModel
            {
                TeacherCount = this.teacherServises.GetCount(),
                CourseCount = this.coursesServices.GetCount(),
                StudentCount = this.studentsServices.GetCount(),
                ParentCount = this.parentServices.GetCount(),
            };
            return this.View(viewModel);
        }
    }
}

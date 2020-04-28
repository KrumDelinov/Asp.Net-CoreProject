namespace ESchool.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using ESchool.Data;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels;
    using ESchool.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IClassroomsServices classroomsServices;

        public HomeController(IClassroomsServices classroomsServices)
        {
            this.classroomsServices = classroomsServices;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult HttpError(int errorCode)
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

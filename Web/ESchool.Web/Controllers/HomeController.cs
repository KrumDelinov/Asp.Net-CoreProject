namespace ESchool.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using ESchool.Data;
    using ESchool.Web.ViewModels;
    using ESchool.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var viewModels = new IndexViewModel();
            var subjects = this.dbContext.Subjects.Select(x => new IndexSubjectViewModel
            {
                Description = x.Description,
            }).ToList();
            viewModels.Subjects = subjects;

            return this.View(viewModels);
        }

        public IActionResult Privacy()
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

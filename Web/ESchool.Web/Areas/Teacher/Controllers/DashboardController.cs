using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Web.Areas.Teacher
{
    public class DashboardController : TeacherController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
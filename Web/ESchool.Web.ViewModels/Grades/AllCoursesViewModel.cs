using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Grades
{
    public class AllCoursesViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}

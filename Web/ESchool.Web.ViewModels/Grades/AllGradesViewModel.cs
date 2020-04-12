using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Grades
{
    public class AllGradesViewModel
    {
        public IEnumerable<GradeViewModel> Grades { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Classrooms
{
    public class AllClassroomViewModel
    {
        public IEnumerable<ClassroomViewModel> Classrooms { get; set; }
    }
}

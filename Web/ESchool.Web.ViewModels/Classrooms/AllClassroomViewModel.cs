namespace ESchool.Web.ViewModels.Classrooms
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllClassroomViewModel
    {
        public IEnumerable<ClassroomViewModel> Classrooms { get; set; }
    }
}

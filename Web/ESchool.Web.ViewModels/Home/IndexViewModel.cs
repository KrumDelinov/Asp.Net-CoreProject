namespace ESchool.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexClassroomViewModel> Classrooms { get; set; }
    }
}

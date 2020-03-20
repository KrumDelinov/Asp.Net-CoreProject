namespace ESchool.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<IndexGradeViewModel> Classrooms { get; set; }
    }
}

using ESchool.Web.ViewModels.Grades;
using ESchool.Web.ViewModels.Teachers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Students
{
    public class IndexStudentViewModel : StudentViewModel
    {
        public IEnumerable<StudentAllExams> AllExams { get; set; }

        public IEnumerable<StudentTaechers> StudentTeachers { get; set; }
    }
}

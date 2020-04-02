using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Classrooms
{
    public class ClassroomViewModel : IMapFrom<Classroom>
    {
        public int Id { get; set; }

        public string NumberDescription { get; set; }

        public string TeacherFirstName { get; set; }

        //public string TeacherLastName { get; set; }

        //public string FullName { get; set; }

        public string TeacherSubjectDescription { get; set; }
    }
}

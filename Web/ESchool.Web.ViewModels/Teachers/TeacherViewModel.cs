﻿namespace ESchool.Web.ViewModels.Teachers
{
    using System.Collections.Generic;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Grades;

    public class TeacherViewModel : IMapFrom<Teacher>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string SubjectDescription { get; set; }

        public string UserUserName { get; set; }

        public virtual IEnumerable<CourseViewModel> TeacherCourses { get; set; }
    }
}

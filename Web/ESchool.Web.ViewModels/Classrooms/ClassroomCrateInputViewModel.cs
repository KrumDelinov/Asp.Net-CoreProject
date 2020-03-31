﻿namespace ESchool.Web.ViewModels.Classrooms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class ClassroomCrateInputViewModel : IMapFrom<Classroom>
    {
        [Required]
        [Display(Name ="Room Number")]
        public string NumberDescription { get; set; }

        [Required]
        [Display(Name = "Teachers")]
        public int TeacherId { get; set; }

        public IEnumerable<TeacherDropDownViewModel> Teachers { get; set; }
    }
}
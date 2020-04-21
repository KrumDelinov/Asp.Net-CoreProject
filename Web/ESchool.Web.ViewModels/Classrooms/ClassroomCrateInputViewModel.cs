namespace ESchool.Web.ViewModels.Classrooms
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class ClassroomCrateInputViewModel : IMapFrom<Classroom>
    {
        [Required]
        [Display(Name ="Номер стая")]
        public int Number { get; set; }

        [Required]
        [Display(Name = "Описание кабинет")]
        public string Description { get; set; }

        [Display(Name = "Учител")]
        public int TeacherId { get; set; }

        public IEnumerable<TeacherDropDownViewModel> Teachers { get; set; }
    }
}

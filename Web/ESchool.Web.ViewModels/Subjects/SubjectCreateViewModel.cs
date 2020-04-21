using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ESchool.Web.ViewModels.Subjects
{
    public class SubjectCreateViewModel : IMapFrom<Subject>
    {
        [Required]
        [Display(Name = "Предмет")]
        public string Description { get; set; }
    }
}

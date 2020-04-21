namespace ESchool.Web.ViewModels.Attendances
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AttendancesCreateViewModel : IMapFrom<Attendance>
    {
        [Required]
        [StringLength(300)]
        public string Remark { get; set; }

        public int StudentId { get; set; }

        public int TeacherId { get; set; }
    }
}

namespace ESchool.Web.ViewModels.Attendances
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class AttendancesCreateViewModel : IMapFrom<Attendance>
    {
        public string Remark { get; set; }

        public int StudentId { get; set; }

        public int TeacherId { get; set; }
    }
}

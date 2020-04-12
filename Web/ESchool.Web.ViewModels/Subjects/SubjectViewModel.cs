using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Subjects
{
    public class SubjectViewModel : IMapFrom<Subject>
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}

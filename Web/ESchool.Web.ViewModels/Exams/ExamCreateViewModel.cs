using AutoMapper;
using ESchool.Data.Models;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESchool.Web.ViewModels.Exams
{
    public class ExamCreateViewModel : IMapFrom<Exam>, IHaveCustomMappings
    {
        public string ExamType { get; set; }

        public decimal Result { get; set; }

        public int TeacherId { get; set; }

        public int StudentId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Student, ExamCreateViewModel>().ForMember(
             m => m.StudentId,
             opt => opt.MapFrom(x => x.Id));
        }
    }
}

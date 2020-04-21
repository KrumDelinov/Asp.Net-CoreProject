namespace ESchool.Web.ViewModels.Exams
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class ExamCreateViewModel : IMapFrom<Exam>, IHaveCustomMappings
    {
        [Display(Name = "Вид изпит")]
        public string ExamType { get; set; }

        [Required]
        [Range(2.00, 6.00, ErrorMessage ="Между 2 и 6")]
        [Display(Name = "Оценка")]
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

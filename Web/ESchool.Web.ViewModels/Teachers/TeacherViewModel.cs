namespace ESchool.Web.ViewModels.Teachers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class TeacherViewModel : IMapFrom<Teacher>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SubjectDescription { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Teacher, TeacherViewModel>().ForMember(
                m => m.SubjectDescription,
                opt => opt.MapFrom(x => x.Subject.Description));
        }
    }
}

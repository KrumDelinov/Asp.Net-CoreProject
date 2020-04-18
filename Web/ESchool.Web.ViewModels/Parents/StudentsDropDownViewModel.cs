namespace ESchool.Web.ViewModels.Parents
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ESchool.Data.Models;
    using ESchool.Services.Mapping;
    using ESchool.Web.ViewModels.Students;

    public class StudentsDropDownViewModel : StudentViewModel
    {

        public int CourseId { get; set; }

    }
}
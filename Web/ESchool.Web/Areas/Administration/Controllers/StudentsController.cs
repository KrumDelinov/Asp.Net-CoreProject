﻿namespace ESchool.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Students;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class StudentsController : AdministrationController
    {
        private readonly IExamsServices examsServices;
        private readonly ICoursesServices courseServices;
        private readonly IStudentsServices studentsServices;

        public StudentsController(IExamsServices examsServices, ICoursesServices courseServices, IStudentsServices studentsServices)
        {
            this.examsServices = examsServices;
            this.courseServices = courseServices;
            this.studentsServices = studentsServices;
        }

        public IActionResult All()
        {
            var students = this.studentsServices.GetAll<StudentViewModel>();

            var viewModel = new AllStudentsViewModel
            {
                Students = students,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {

            var viewModel = this.studentsServices.Student<StudentViewModel>(id);
 
            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var courses = this.courseServices.GetAll<CourseDropDownViewModel>();
            var viewModel = new StudentsCreateViewModel();
            viewModel.Courses = courses;
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentsCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                inputModel.Courses = this.courseServices.GetAll<CourseDropDownViewModel>();
                return this.View(inputModel);
            }

            var studentId = await this.studentsServices.CreateAsync(inputModel.FirstName, inputModel.LastName, inputModel.BirthDate, inputModel.CourseId);
            await this.courseServices.AddStudetToCourse(inputModel.CourseId, studentId);
            return this.RedirectToAction("Details", "Courses", new { id = inputModel.CourseId });
        }

        public IActionResult Exams(int id)
        {
            var exams = this.examsServices.GetAllStuentExans<StudentAllExams>(id);
            var attendances = this.studentsServices.GetAllStudentAttendaces<StudentAttendances>(id);

            var hasExams = exams.Count();
            if (hasExams == 0)
            {

                return this.View("NotFound");

            }

            var viewModel = new StudentAllRolesViewModel
            {
                Attendances = attendances,
                StudentExams = exams,
            };
            return this.View(viewModel);

        }


        public IActionResult Attendances(int id)
        {
            var attendances = this.studentsServices.GetAllStudentAttendaces<StudentAttendances>(id);

            var viewModel = new StudentAllRolesViewModel
            {
                Attendances = attendances,
            };

            return this.View(viewModel);
        }

        public IActionResult StudentDetails(int id)
        {
            StudentAllRolesViewModel viewModel = this.StudentViewModel(id);

            return this.View(viewModel);
        }

        private StudentAllRolesViewModel StudentViewModel(int id)
        {
            var exams = this.examsServices.GetAllStuentExans<StudentAllExams>(id);
            var attendances = this.studentsServices.GetAllStudentAttendaces<StudentAttendances>(id);
            var viewModel = new StudentAllRolesViewModel
            {
                Attendances = attendances,
                StudentExams = exams,
            };
            return viewModel;
        }

    }
}

namespace ESchool.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.ViewModels.Classrooms;
    using ESchool.Web.ViewModels.Grades;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class GradesController : AdministrationController
    {
        private readonly IGradesServices gradesServices;
        private readonly ITeacherServises teacherServises;

        public GradesController(IGradesServices gradesServices, ITeacherServises teacherServises)
        {
            this.gradesServices = gradesServices;
            this.teacherServises = teacherServises;
        }

        public IActionResult Create()
        {
            var techers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new GradeCreateInputViewModel();
            viewModel.Teachers = techers;
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(GradeCreateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var gradeId = await this.gradesServices.CreateAsync(input.Issue, input.Description, input.TeacherId);

            await this.teacherServises.SetGradeToTeacher(input.TeacherId);

            return this.RedirectToAction("Details", new { id = gradeId });
        }

        public IActionResult All()
        {
            var grades = this.gradesServices.GetAll<GradeViewModel>();
            var viewModel = new AllGradesViewModel
            {
                Grades = grades,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            GradeViewModel viewModel = this.gradesServices.Grade<GradeViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var grade = this.gradesServices.GetGrade(id);
            var teachers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new GradeEditViewModel
            {
                Id = grade.Id,
                Issue = grade.Issue,
                Description = grade.Description,
                TeacherId = grade.TeacherId,
                Teachers = teachers,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GradeEditViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var grade = this.gradesServices.GetGrade(viewModel.Id);

            await this.teacherServises.RemoveGradeFromTeacher(grade.TeacherId);

            grade.Issue = viewModel.Issue;
            grade.Description = viewModel.Description;
            grade.TeacherId = viewModel.TeacherId;

            await this.gradesServices.UpdateGrade(grade);
            await this.teacherServises.SetClassroomToTeacher(viewModel.TeacherId);

            return this.RedirectToAction("Details", new { id = viewModel.Id });
        }

        public IActionResult Delete(int id)
        {
            GradeViewModel viewModel = this.gradesServices.Grade<GradeViewModel>(id);

            if (!this.ModelState.IsValid)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = this.gradesServices.GetGrade(id);
            var teacher = this.teacherServises.GetTeacher(grade.TeacherId);
            teacher.HasGrade = false;
            await this.teacherServises.UpdateTeacher(teacher);
            await this.gradesServices.DeleteGrade(grade);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
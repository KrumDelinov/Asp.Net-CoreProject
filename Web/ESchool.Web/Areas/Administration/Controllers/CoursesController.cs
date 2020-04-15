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

    public class CoursesController : AdministrationController
    {
        private readonly ICoursesServices coursesServices;
        private readonly ITeacherServises teacherServises;

        public CoursesController(ICoursesServices gradesServices, ITeacherServises teacherServises)
        {
            this.coursesServices = gradesServices;
            this.teacherServises = teacherServises;
        }

        public IActionResult Create()
        {
            var techers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new CourseCreateInputViewModel();
            viewModel.Teachers = techers;
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(CourseCreateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var courseId = await this.coursesServices.CreateAsync(input.Issue, input.Description, input.TeacherId);

            await this.teacherServises.SetGradeToTeacher(input.TeacherId);

            return this.RedirectToAction("Details", new { id = courseId });
        }

        public IActionResult AddTeachersToCourse(int id)
        {
            var course = this.coursesServices.GetCourse(id);
            var teachers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new CourseTeachersViewModel
            {
                Id = course.Id,
                Issue = course.Issue,
                Description = course.Description,
                TeacherId = course.TeacherId,
                Teachers = teachers,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeachersToCourse(CourseTeachersViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            await this.coursesServices.AddTeachersToCourse(viewModel.Id, viewModel.TeacherId);

            return this.RedirectToAction("Details", new { id = viewModel.Id });
        }

        public IActionResult All()
        {
            var courses = this.coursesServices.GetAll<CourseViewModel>();
            var viewModel = new AllCoursesViewModel
            {
                Courses = courses,
            };

            return this.View(viewModel);
        }

        public IActionResult Details(int id)
        {
            CourseViewModel viewModel = this.coursesServices.Course<CourseViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var course = this.coursesServices.GetCourse(id);
            var teachers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new CourseEditViewModel
            {
                Id = course.Id,
                Issue = course.Issue,
                Description = course.Description,
                TeacherId = course.TeacherId,
                Teachers = teachers,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseEditViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var course = this.coursesServices.GetCourse(viewModel.Id);

            await this.teacherServises.RemoveGradeFromTeacher(course.TeacherId);

            course.Issue = viewModel.Issue;
            course.Description = viewModel.Description;
            course.TeacherId = viewModel.TeacherId;

            await this.coursesServices.UpdateCourse(course);
            await this.teacherServises.SetClassroomToTeacher(viewModel.TeacherId);

            return this.RedirectToAction("Details", new { id = viewModel.Id });
        }

        public IActionResult Delete(int id)
        {
            CourseViewModel viewModel = this.coursesServices.Course<CourseViewModel>(id);

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
            var grade = this.coursesServices.GetCourse(id);
            var teacher = this.teacherServises.GetTeacher(grade.TeacherId);
            teacher.HasGrade = false;
            await this.teacherServises.UpdateTeacher(teacher);
            await this.coursesServices.DeleteCourse(grade);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
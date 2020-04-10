namespace ESchool.Web.Controllers
{
    using System.Threading.Tasks;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Web.Areas.Administration.Controllers;
    using ESchool.Web.ViewModels.Classrooms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ClassroomsController : AdministrationController
    {
        private readonly IClassroomsServices classroomsServices;
        private readonly ITeacherServises teacherServises;

        public ClassroomsController(IClassroomsServices classroomsServices, ITeacherServises teacherServises)
        {
            this.classroomsServices = classroomsServices;
            this.teacherServises = teacherServises;
        }

        public IActionResult Create()
        {
            var techers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new ClassroomCrateInputViewModel();
            viewModel.Teachers = techers;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(ClassroomCrateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var classroomId = await this.classroomsServices.CreateAsync(input.Number, input.Description, input.TeacherId);

            await this.teacherServises.SetClassroomToTeacher(input.TeacherId);

            return this.RedirectToAction("All");
        }

        public IActionResult All()
        {
            var classroom = this.classroomsServices.GetAll<ClassroomViewModel>();
            var viewModel = new AllClassroomViewModel
            {
                Classrooms = classroom,
            };

            return this.View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var classroom = this.classroomsServices.GetClassroom(id);
            var teachers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new ClassroomEditViewModel
            {
               Id = classroom.Id,
               Number = classroom.Number,
               Description = classroom.Description,
               TeacherId = classroom.TeacherId,
               Teachers = teachers,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClassroomEditViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }

            var classroom = this.classroomsServices.GetClassroom(viewModel.Id);

            await this.teacherServises.RemoveClassroomFromTeacher(classroom.TeacherId);

            classroom.Number = viewModel.Number;
            classroom.Description = viewModel.Description;
            classroom.TeacherId = viewModel.TeacherId;

            await this.classroomsServices.UpdateClassroom(classroom);
            await this.teacherServises.SetClassroomToTeacher(viewModel.TeacherId);

            return this.RedirectToAction("Details", new { id = viewModel.Id });
        }

        public IActionResult Details(int id)
        {
            ClassroomViewModel viewModel = this.classroomsServices.Classroom<ClassroomViewModel>(id);

            return this.View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            ClassroomViewModel viewModel = this.classroomsServices.Classroom<ClassroomViewModel>(id);

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
            var classroom = this.classroomsServices.GetClassroom(id);
            var teacher = this.teacherServises.GetTeacher(classroom.TeacherId);
            teacher.HasClassroom = false;
            await this.teacherServises.UpdateTeacher(teacher);
            await this.classroomsServices.DeleteClassroom(classroom);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}

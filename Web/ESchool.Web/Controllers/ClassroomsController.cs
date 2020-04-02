namespace ESchool.Web.Controllers
{
    using System.Threading.Tasks;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Web.ViewModels.Classrooms;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ClassroomsController : BaseController
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

            var classroomId = await this.classroomsServices.CreateAsync(input.NumberDescription, input.TeacherId);

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

        [Authorize]
        public IActionResult Edit(int id)
        {

            var classroom = this.classroomsServices.GetClassroom(id);

            var techers = this.teacherServises.GetAll<TeacherDropDownViewModel>();
            var viewModel = new ClassroomEditViewModel
            {
                Id = id,
                NumberDescription = classroom.NumberDescription,
                Teachers = techers,
            };
            return this.View(viewModel);
        }
    }
}

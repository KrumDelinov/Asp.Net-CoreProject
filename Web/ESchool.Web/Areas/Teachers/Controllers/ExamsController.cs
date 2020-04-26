namespace ESchool.Web.Areas.Teachers.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Services.Data.Contracts;
    using ESchool.Web.Areas.Teacher;
    using ESchool.Web.ViewModels.Exams;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Teachers")]
    public class ExamsController : TeachersController
    {
        private readonly ApplicationDbContext _context;
        private readonly IExamsServices examsServices;
        private readonly ITeacherServises teacherServises;
        private readonly IStudentsServices studentsServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public ExamsController(
            ApplicationDbContext context,
            IExamsServices examsServices,
            ITeacherServises teacherServises,
            IStudentsServices studentsServices,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            this.examsServices = examsServices;
            this.teacherServises = teacherServises;
            this.studentsServices = studentsServices;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: Teachers/Exams
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exams.Include(e => e.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teachers/Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Teachers/Exams/Create

        public IActionResult Create(int id)
        {
            var viewModel = new ExamCreateViewModel
            {
                StudentId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ExamCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var studentId = inputModel.StudentId;

            var courseId = this.studentsServices.GetStudenCourseId(studentId);

            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);

            var techer = this.teacherServises.GetUserTeacher(userId);

            var examId = await this.examsServices.CreateAsync(inputModel.ExamType, inputModel.Result, techer.Id);

            await this.examsServices.AddExamToStudent(studentId, examId);

            return this.RedirectToAction("Details", "Dashboard", new { id = courseId });
        }

        // GET: Teachers/Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", exam.TeacherId);
            return View(exam);
        }

        // POST: Teachers/Exams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExamType,Result,TeacherId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", exam.TeacherId);
            return View(exam);
        }

        // GET: Teachers/Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Teachers/Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}

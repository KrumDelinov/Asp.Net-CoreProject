namespace ESchool.Web.Areas.Administration.Controllers
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
        private readonly ApplicationDbContext _context;
        private readonly ICoursesServices courseServices;
        private readonly IStudentsServices studentsServices;

        public StudentsController(ApplicationDbContext context, ICoursesServices courseServices, IStudentsServices studentsServices)
        {
            _context = context;
            this.courseServices = courseServices;
            this.studentsServices = studentsServices;
        }

        // GET: Administration/Students 
        public async Task<IActionResult> All()
        {
            var applicationDbContext = _context.Students.Include(s => s.Course).Include(s => s.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return this.View(student);
        }

        // GET: Administration/Students/Create
        public IActionResult Create()
        {
            var courses = this.courseServices.GetAll<CourseDropDownViewModel>();
            var viewModel = new StudentsCreateViewModel();
            viewModel.Courses = courses;
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentsCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var studentId = await this.studentsServices.CreateAsync(inputModel.FirstName, inputModel.LastName, inputModel.BirthDate, inputModel.CourseId);
            await this.courseServices.AddStudetToCourse(inputModel.CourseId, studentId);
            return this.RedirectToAction("Details", "Courses", new { id = inputModel.CourseId });
        }

        // GET: Administration/Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_context.Courses, "Id", "Id", student.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", student.UserId);
            return View(student);
        }

        // POST: Administration/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,UserId,GradeId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["GradeId"] = new SelectList(_context.Courses, "Id", "Id", student.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", student.UserId);
            return View(student);
        }

        // GET: Administration/Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Administration/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}

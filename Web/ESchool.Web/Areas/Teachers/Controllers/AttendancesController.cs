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
    using ESchool.Web.ViewModels.Attendances;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class AttendancesController : TeachersController
    {
        private readonly ApplicationDbContext _context;
        private readonly ITeacherServises teacherServises;
        private readonly IStudentsServices studentsServices;
        private readonly IAtendacesServices atendacesServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;

        public AttendancesController(
            ApplicationDbContext context,
            ITeacherServises teacherServises,
            IStudentsServices studentsServices,
            IAtendacesServices atendacesServices,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            this.teacherServises = teacherServises;
            this.studentsServices = studentsServices;
            this.atendacesServices = atendacesServices;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        // GET: Teachers/Attendances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attendances.Include(a => a.Student).Include(a => a.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Teachers/Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        public IActionResult Create(int id)
        {
            var viewModel = new AttendancesCreateViewModel
            {
                StudentId = id,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AttendancesCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var studentId = inputModel.StudentId;

            var user = await this.userManager.GetUserAsync(this.User);

            var userId = await this.userManager.GetUserIdAsync(user);

            var techer = this.teacherServises.GetUserTeacher(userId);

            var attendanceId = await this.atendacesServices.CreateAsync(inputModel.Remark, inputModel.StudentId, techer.Id);

            await this.studentsServices.AddAttendanceToStudent(studentId, attendanceId);

            return this.RedirectToAction("Details", new { id = attendanceId });
        }

        // GET: Teachers/Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FirstName", attendance.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", attendance.TeacherId);
            return View(attendance);
        }

        // POST: Teachers/Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Remark,StudentId,TeacherId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.Id))
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
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FirstName", attendance.StudentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "FirstName", attendance.TeacherId);
            return View(attendance);
        }

        // GET: Teachers/Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Teachers/Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }
    }
}

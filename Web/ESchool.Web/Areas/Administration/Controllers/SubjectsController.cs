using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ESchool.Data;
using ESchool.Data.Models;
using ESchool.Web.Areas.Administration.Controllers;
using ESchool.Web.ViewModels.Subjects;
using ESchool.Services.Data;

namespace ESchool.Web.Controllers
{
    public class SubjectsController : AdministrationController
    {
        private readonly ApplicationDbContext _context;
        private readonly ISubjectsServices subjectsServices;

        public SubjectsController(ApplicationDbContext context, ISubjectsServices subjectsServices)
        {
            _context = context;
            this.subjectsServices = subjectsServices;
        }

        // GET: Subjects
        public async Task<IActionResult> All()
        {


            return View(await _context.Subjects.ToListAsync());
        }

        // GET: Subjects/Details/5
        public IActionResult Details(int id)
        {

            SubjectViewModel viewModel = this.subjectsServices.Subject<SubjectViewModel>(id);

            return this.View(viewModel);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            var viewModel = new SubjectCreateViewModel();
            return this.View(viewModel);
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            //var user = await this.userManager.GetUserAsync(this.User);

            //var role = roleManager.GetRoleNameAsync();
            var subjectId = await this.subjectsServices.CreateAsync(inputModel.Description);

            return this.RedirectToAction("Details", new { id = subjectId });
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectExists(subject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(All));
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            SubjectViewModel viewModel = this.subjectsServices.Subject<SubjectViewModel>(id);

            return this.View(viewModel);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = this.subjectsServices.GetSubject(id);

            await this.subjectsServices.DeleteSubject(subject);
            return this.RedirectToAction(nameof(this.All));
        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}

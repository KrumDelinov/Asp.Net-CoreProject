namespace ESchool.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data;
    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Web.Areas.Administration.Controllers;
    using ESchool.Web.ViewModels.Subjects;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

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

        public IActionResult Create()
        {
            var viewModel = new SubjectCreateViewModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectCreateViewModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var subjectId = await this.subjectsServices.CreateAsync(inputModel.Description);

            return this.RedirectToAction("Details", new { id = subjectId });
        }

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

  
        [HttpPost]
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

   

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}

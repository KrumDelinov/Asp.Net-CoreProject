namespace ESchool.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;
    using ESchool.Services.Data;
    using ESchool.Web.ViewModels.Teachers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class Taechers2Controller : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITeacherServises teacherServises;

        public Taechers2Controller(UserManager<ApplicationUser> userManager, ITeacherServises teacherServises)
        {
            this.userManager = userManager;
            this.teacherServises = teacherServises;
        }

        // GET: Taechers
        public IActionResult Index()
        {
            return this.View();
        }

        // GET: Taechers/Details/5
        public IActionResult Details(int id)
        {
            return this.View();
        }

        // GET: Taechers/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Taechers/Create
        [HttpPost]

        public async Task<IActionResult> CreateAsync(TeacherCreateInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            //var user = await this.userManager.GetUserAsync(this.User);
            var teacherId = await this.teacherServises.CreateAsync(inputModel.FirstName, inputModel.LastName, inputModel.SubjectId);

            return this.RedirectToAction("Details", new { id = teacherId });
        }

        // GET: Taechers/Edit/5
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        // POST: Taechers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View();
            }
        }

        // GET: Taechers/Delete/5
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        // POST: Taechers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return this.RedirectToAction(nameof(this.Index));
            }
            catch
            {
                return this.View();
            }
        }
    }
}

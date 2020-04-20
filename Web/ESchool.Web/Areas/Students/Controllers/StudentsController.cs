namespace ESchool.Web.Areas.Students.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.StudentRoleName)]
    [Area("Students")]
    public class StudentsController : BaseController
    {
    }
}
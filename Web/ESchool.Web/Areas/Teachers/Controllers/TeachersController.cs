namespace ESchool.Web.Areas.Teacher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    [Area("Teachers")]
    public class TeachersController : BaseController
    {
    }
}

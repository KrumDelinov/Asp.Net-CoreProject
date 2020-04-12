using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESchool.Common;
using ESchool.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ESchool.Web.Areas.Teacher
{
    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    [Area("Teacher")]
    public class TeacherController : BaseController
    {
       
    }
}
namespace ESchool.Web.Areas.Parents.Controllers
{

    using ESchool.Common;
    using ESchool.Web.Controllers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.ParentRoleName + "," + GlobalConstants.TeacherRoleName)]
    [Area("Parents")]
    public class ParentsController : BaseController
    {
    }
}
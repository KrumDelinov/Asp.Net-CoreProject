namespace ESchool.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);

            await SeedRoleAsync(roleManager, GlobalConstants.TeacherRoleName);

            await SeedRoleAsync(roleManager, GlobalConstants.StudentRoleName);

            await SeedRoleAsync(roleManager, GlobalConstants.ParentRoleName);

            //await SeedToRoleAsync(userManager, GlobalConstants.TeacherRoleName, dbContext);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        //private static async Task SeedToRoleAsync(UserManager<ApplicationUser> userManager, string rolleName, ApplicationDbContext dbContext)
        //{
        //    var user = dbContext.Users.Where(x => x.Email == "dimitrov@gmail.bg").FirstOrDefault();
            
           
            
        //    if (user != null)
        //    {
        //        /*var result = */
        //        await userManager.AddToRoleAsync(user, rolleName);
        //        //if (!result.Succeeded)
        //        //{
        //        //    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        //        //}
        //    }
        //}
    }
}

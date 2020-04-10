namespace ESchool.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Common;
    using ESchool.Data.Models;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    internal class RolesSeeder : ISeeder
    {
        private const string Teacher = "Teacher";
        private const string Student = "Student";
        private const string Parent = "Parent";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(roleManager, userManager, GlobalConstants.AdministratorRoleName);

            await SeedRoleAsync(roleManager, userManager, GlobalConstants.TeacherRoleName);

            await SeedRoleAsync(roleManager, userManager, GlobalConstants.StudentRoleName);

            await SeedRoleAsync(roleManager, userManager, GlobalConstants.ParentRoleName);

            //await SeedToRoleAsync(userManager, GlobalConstants.TeacherRoleName, dbContext);
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, string roleName)
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

            if (!await userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                };

                var password = "123456";

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                }

                //for (int i = 1; i <= 15; i++)
                //{
                //    if (i <= 5)
                //    {
                //        var tempUser = new ApplicationUser
                //        {
                //            UserName = $"{Teacher}{i}@gmail.com",
                //            Email = $"{Teacher}{i}@gmail.com",
                //            EmailConfirmed = true,
                //        };
                //        var tempResult = await userManager.CreateAsync(user, password);
                //    }
                //    else if (i > 5 && i <= 10)
                //    {
                //        var tempUser = new ApplicationUser
                //        {
                //            UserName = $"{Student}{i}@gmail.com",
                //            Email = $"{Student}{i}@gmail.com",
                //            EmailConfirmed = true,
                //        };
                //        var tempResult = await userManager.CreateAsync(user, password);
                //    }
                //    else
                //    {
                //        var tempUser = new ApplicationUser
                //        {
                //            UserName = $"{Parent}{i}@gmail.com",
                //            Email = $"{Parent}{i}@gmail.com",
                //            EmailConfirmed = true,
                //        };
                //        var tempResult = await userManager.CreateAsync(user, password);
                //    }

                //}
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

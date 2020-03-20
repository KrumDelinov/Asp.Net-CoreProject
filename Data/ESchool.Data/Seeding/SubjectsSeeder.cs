namespace ESchool.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public class SubjectsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Subjects.Any())
            {
                return;
            }

            List<string> subjects = new List<string> { "Biology", "Mathematics", "Geography", "English", "History", "Information Technology", "Art" };

            foreach (var item in subjects)
            {
                await dbContext.Subjects.AddAsync(new Subject
                {
                    Description = item,
                });
            }
        }
    }
}

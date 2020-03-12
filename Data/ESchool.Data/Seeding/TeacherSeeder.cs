namespace ESchool.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public class TeacherSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Teachers.Any())
            {
                return;
            }

            var teacher = new Teacher
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                SubjectId = 5,
                HasClassroom = false,
            };

            await dbContext.Teachers.AddAsync(teacher);
        }
    }
}

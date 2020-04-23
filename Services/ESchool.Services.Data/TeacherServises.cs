namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class TeacherServises : ITeacherServises
    {
        private readonly IDeletableEntityRepository<Teacher> techerRepository;

        public TeacherServises(IDeletableEntityRepository<Teacher> techerRepository)
        {
            this.techerRepository = techerRepository;
        }

        public async Task<int> CreateAsync(string firstName, string lastName, int subjectId, string email = null)
        {
            var teacher = new Teacher
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                SubjectId = subjectId,
            };

            await this.techerRepository.AddAsync(teacher);
            await this.techerRepository.SaveChangesAsync();
            return teacher.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.techerRepository.All().To<T>().ToList();
        }

        public int GetCount()
        {
            return this.techerRepository.All().Count();
        }

        public Teacher GetTeacher(int id)
        {
            var teacher = this.techerRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return teacher;
        }

        public async Task<int> SetClassroomToTeacher(int id)
        {
            var teacher = this.techerRepository.All().Where(x => x.Id == id).FirstOrDefault();
            teacher.HasClassroom = true;
            await this.techerRepository.SaveChangesAsync();
            return teacher.Id;
        }

        public T Teacher<T>(int id)
        {
            var teacher = this.techerRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return teacher;
        }

        public async Task UpdateTeacher(Teacher teacher)
        {
            this.techerRepository.Update(teacher);
            await this.techerRepository.SaveChangesAsync();
        }

        public async Task DeleteTeacher(Teacher teacher)
        {
            this.techerRepository.Delete(teacher);

            await this.techerRepository.SaveChangesAsync();
        }

        public async Task<int> RemoveClassroomFromTeacher(int id)
        {
            var teacher = this.techerRepository.All().Where(x => x.Id == id).FirstOrDefault();
            teacher.HasClassroom = false;
            await this.techerRepository.SaveChangesAsync();
            return teacher.Id;
        }

        public async Task<int> SetGradeToTeacher(int id)
        {
            var teacher = this.techerRepository.All().Where(x => x.Id == id).FirstOrDefault();
            teacher.HasGrade = true;
            await this.techerRepository.SaveChangesAsync();
            return teacher.Id;
        }

        public async Task<int> RemoveGradeFromTeacher(int id)
        {
            var teacher = this.techerRepository.All().Where(x => x.Id == id).FirstOrDefault();
            teacher.HasGrade = false;
            await this.techerRepository.SaveChangesAsync();
            return teacher.Id;
        }

        public Teacher GetUserTeacher(string userId)
        {
            var techer = this.techerRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            return techer;
        }

        public async Task AddUserToTeacher(int techerId, string userId)
        {
            var techer = this.techerRepository.All().Where(x => x.Id == techerId).FirstOrDefault();
            techer.UserId = userId;
            await this.techerRepository.SaveChangesAsync();
        }
    }
}

namespace ESchool.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Data.Contracts;
    using ESchool.Services.Mapping;

    public class GradesServices : IGradesServices
    {
        private readonly IDeletableEntityRepository<Grade> gradeRepository;
        private readonly IDeletableEntityRepository<Teacher> teacherRepository;
        private readonly IStudentsServices studentsServices;

        public GradesServices(
            IDeletableEntityRepository<Grade> gradeRepository,
            IDeletableEntityRepository<Teacher> teacherRepository,
            IStudentsServices studentsServices)
        {
            this.gradeRepository = gradeRepository;
            this.teacherRepository = teacherRepository;
            this.studentsServices = studentsServices;
        }

        public async Task<int> CreateAsync(int issue, string description, int techerId)
        {
            var grade = new Grade
            {
                Issue = issue,
                Description = description,
                TeacherId = techerId,

            };

            await this.gradeRepository.AddAsync(grade);
            await this.gradeRepository.SaveChangesAsync();

            return grade.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.gradeRepository.All().OrderBy(x => x.Issue).To<T>().ToList();
        }

        public Grade GetGrade(int id)
        {
            var grade = this.gradeRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return grade;
        }

        public async Task UpdateGrade(Grade grade)
        {
            this.gradeRepository.Update(grade);
            await this.gradeRepository.SaveChangesAsync();
        }

        public async Task DeleteGrade(Grade grade)
        {
            this.gradeRepository.Delete(grade);

            await this.gradeRepository.SaveChangesAsync();
        }

        public T Grade<T>(int id)
        {
            var grade = this.gradeRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return grade;
        }

        public async Task AddStudetToGrade(int gradeId, int studentId)
        {
            var grade = this.GetGrade(gradeId);

            var student = this.studentsServices.GetStudent(studentId);

            grade.Students.Add(student);
            await this.gradeRepository.SaveChangesAsync();
        }
    }
}

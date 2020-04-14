using ESchool.Data.Common.Repositories;
using ESchool.Data.Models;
using ESchool.Services.Data.Contracts;
using ESchool.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data
{
    public class StudentsServices : IStudentsServices
    {
        private readonly EfDeletableEntityRepository<Student> studentRepository;
        private readonly EfDeletableEntityRepository<Course> gradeRepository;

        public StudentsServices(EfDeletableEntityRepository<Student> studentRepository, EfDeletableEntityRepository<Course> gradeRepository)
        {
            this.studentRepository = studentRepository;
            this.gradeRepository = gradeRepository;
        }

        public async Task<int> CreateAsync(string firstName, string lastName, DateTime birthDate, int gradeId)
        {
            var student = new Student
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                CourseId = gradeId,
            };
            
            await this.studentRepository.AddAsync(student);
            await this.studentRepository.SaveChangesAsync();

            return student.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public Student GetStudent(int id)
        {
            var student = this.studentRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return student;
        }

        public T Student<T>(int id)
        {
            var student = this.studentRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return student;
        }
    }
}

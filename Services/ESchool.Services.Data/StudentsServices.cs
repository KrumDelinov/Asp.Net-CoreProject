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
        private readonly IDeletableEntityRepository<Student> studentRepository;
        private readonly IDeletableEntityRepository<Grade> gradeRepository;

        public StudentsServices(IDeletableEntityRepository<Student> studentRepository, IDeletableEntityRepository<Grade> gradeRepository)
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
                GradeId = gradeId,
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

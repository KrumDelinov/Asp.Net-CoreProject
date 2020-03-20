﻿namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<int> CreateAsync(string firstName, string lastName, string userId, int subjectId)
        {
            var teacher = new Teacher
            {
                FirstName = firstName,
                LastName = lastName,
                UserId = userId,
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

        public Teacher Teacher(int id)
        {
            var teacher = this.techerRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return teacher;
        }
    }
}

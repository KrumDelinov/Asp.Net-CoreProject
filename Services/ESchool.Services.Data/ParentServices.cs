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

    public class ParentServices : IParentServices
    {
        private readonly IDeletableEntityRepository<Parent> parentRepository;
        private readonly IRepository<ParentStudent> parentStudentRepository;

        public ParentServices(IDeletableEntityRepository<Parent> parentRepository, IRepository<ParentStudent> parentStudentRepository)
        {
            this.parentRepository = parentRepository;
            this.parentStudentRepository = parentStudentRepository;
        }

        public async Task<int> CreateAsync(string firstName, string lastName, string email, string userId)
        {
            Parent parent = new Parent
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserId = userId ?? null,
            };

            await this.parentRepository.AddAsync(parent);
            await this.parentRepository.SaveChangesAsync();

            return parent.Id;
        }


        public int GetParentId(string email)
        {
            var parentId = this.parentRepository.All()
                .Where(x => x.Email == email)
                .Select(x => x.Id)
                .FirstOrDefault();
            return parentId;
        }

        public Parent GetUserParent(string userId)
        {
            var parent = this.parentRepository.All().FirstOrDefault(x => x.UserId == userId);
            return parent;

        }

        public bool HasParent(string email)
        {
            bool result = this.parentRepository.All().Any(x => x.Email == email);
            return result;
        }

        public bool ParentHasStudent(string email, int studentId)
        {
            var result = this.parentStudentRepository.All()
                .Any(p => p.Parent.Email == email && p.StudentId == studentId);

            return result;
        }

        public async Task SetStudentToParent(int parentId, int studentId)
        {
            ParentStudent parentsStudent = new ParentStudent
            {
                StudentId = studentId,
                ParentId = parentId,
            };

            await this.parentStudentRepository.AddAsync(parentsStudent);
            await this.parentStudentRepository.SaveChangesAsync();
        }
    }
}

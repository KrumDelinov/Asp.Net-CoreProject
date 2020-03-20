namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SubjectsServices : ISubjectsServices
    {
        private readonly IDeletableEntityRepository<Subject> subjectRepository;

        public SubjectsServices(IDeletableEntityRepository<Subject> techerRepository)
        {
            this.subjectRepository = techerRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.subjectRepository.All().To<T>().ToList();
        }

        public int GetCount()
        {
            return this.subjectRepository.All().Count();
        }
    }
}

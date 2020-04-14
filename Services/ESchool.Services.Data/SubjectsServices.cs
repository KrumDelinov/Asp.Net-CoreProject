namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class SubjectsServices : ISubjectsServices
    {
        private readonly EfDeletableEntityRepository<Subject> subjectRepository;
        private readonly EfDeletableEntityRepository<Teacher> techerRepository;

        public SubjectsServices(EfDeletableEntityRepository<Subject> subjectRepository, EfDeletableEntityRepository<Teacher> techerRepository)
        {
            this.subjectRepository = subjectRepository;
            this.techerRepository = techerRepository;
        }

        public async Task<int> CreateAsync(string description)
        {
            var subject = new Subject
            {
                Description = description,
            };

            await this.subjectRepository.AddAsync(subject);

            await this.subjectRepository.SaveChangesAsync();

            return subject.Id;
        }

        public async Task DeleteSubject(Subject subject)
        {
            //var techers = this.techerRepository.All().Where(s => s.SubjectId == subject.Id).ToList();

            //foreach (var techer in techers)
            //{
            //    techer.Subject = null;
            //    await this.techerRepository.SaveChangesAsync();
            //}

            this.subjectRepository.Delete(subject);

            await this.subjectRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.subjectRepository.All().To<T>().ToList();
        }

        public int GetCount()
        {
            return this.subjectRepository.All().Count();
        }

        public Subject GetSubject(int id)
        {
            var subject = this.subjectRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return subject;
        }

        public T Subject<T>(int id)
        {
            var subject = this.subjectRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return subject;
        }
    }
}

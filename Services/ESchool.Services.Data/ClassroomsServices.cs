namespace ESchool.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class ClassroomsServices : IClassroomsServices
    {
        private readonly EfDeletableEntityRepository<Classroom> classroomsRepository;

        public ClassroomsServices(EfDeletableEntityRepository<Classroom> classroomsRepository)
        {
            this.classroomsRepository = classroomsRepository;
        }

        public async Task<int> CreateAsync(int number, string description, int techerId)
        {
            var classroom = new Classroom
            {
                Number = number,
                Description = description,
                TeacherId = techerId,

            };

            await this.classroomsRepository.AddAsync(classroom);
            await this.classroomsRepository.SaveChangesAsync();

            return classroom.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.classroomsRepository.All().OrderBy(x => x.Number).To<T>().ToList();
        }

        public T Classroom<T>(int id)
        {
            var classroom = this.classroomsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return classroom;
        }

        public Classroom GetClassroom(int id)
        {
            var classroom = this.classroomsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return classroom;
        }

        public async Task UpdateClassroom(Classroom classroom)
        {
            this.classroomsRepository.Update(classroom);
            await this.classroomsRepository.SaveChangesAsync();
        }

        public async Task DeleteClassroom(Classroom classroom)
        {
            this.classroomsRepository.Delete(classroom);

            await this.classroomsRepository.SaveChangesAsync();
        }
    }
}

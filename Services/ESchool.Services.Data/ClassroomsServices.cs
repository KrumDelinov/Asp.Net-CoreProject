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
        private readonly IDeletableEntityRepository<Classroom> classroomsRepository;

        public ClassroomsServices(IDeletableEntityRepository<Classroom> classroomsRepository)
        {
            this.classroomsRepository = classroomsRepository;
        }

        public async Task<int> CreateAsync(string number, int techerId)
        {
            var classroom = new Classroom
            {
                NumberDescription = number,
                TeacherId = techerId,

            };

            await this.classroomsRepository.AddAsync(classroom);
            await this.classroomsRepository.SaveChangesAsync();

            return classroom.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.classroomsRepository.All().To<T>().ToList();
        }
    }
}

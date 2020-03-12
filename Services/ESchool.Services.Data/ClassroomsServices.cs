namespace ESchool.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Services.Mapping;

    public class ClassroomsServices : IClassroomServices
    {
        private readonly IDeletableEntityRepository<Classroom> classroomsRepository;

        public ClassroomsServices(IDeletableEntityRepository<Classroom> classroomsRepository)
        {
            this.classroomsRepository = classroomsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.classroomsRepository.All().To<T>().ToList();
        }
    }
}

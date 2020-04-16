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
    public class AtendacesServices : IAtendacesServices
    {
        private readonly IDeletableEntityRepository<Attendance> attendaceRepository;

        public AtendacesServices(IDeletableEntityRepository<Attendance> attendaceRepository)
        {
            this.attendaceRepository = attendaceRepository;
        }


        public T Attendances<T>(int id)
        {
            var attendance = this.attendaceRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();

            return attendance;
        }

        public async Task<int> CreateAsync(string remark, int studentId, int teacherId)
        {
            var attendace = new Attendance
            {
                Remark = remark,
                StudentId = studentId,
                TeacherId = teacherId,
            };

            await this.attendaceRepository.AddAsync(attendace);
            await this.attendaceRepository.SaveChangesAsync();

            return attendace.Id;
        }

        public Attendance GetAttendance(int id)
        {
           var attendance = this.attendaceRepository.All().Where(x => x.Id == id).FirstOrDefault();

           return attendance;
        }
    }
}

using ESchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IAtendacesServices
    {

        Task<int> CreateAsync(string remark, int studentId, int teacherId);

        T Attendances<T>(int id);

        Attendance GetAttendance(int id);

    }
}

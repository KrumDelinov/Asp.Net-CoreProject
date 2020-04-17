namespace ESchool.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public interface IAtendacesServices
    {
        Task<int> CreateAsync(string remark, int studentId, int teacherId);

        T Attendances<T>(int id);

        Attendance GetAttendance(int id);
    }
}

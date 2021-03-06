﻿namespace ESchool.Services.Data
{
    using ESchool.Data.Models;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IClassroomsServices
    {
        Task<int> CreateAsync(int number, string description, int techerId);

        IEnumerable<T> GetAll<T>();

        T Classroom<T>(int id);

        Classroom GetClassroom(int id);

        Task UpdateClassroom(Classroom classroom);

        Task DeleteClassroom(Classroom classroom);
    }
}

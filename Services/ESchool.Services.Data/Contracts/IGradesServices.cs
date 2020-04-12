using ESchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IGradesServices
    {
        Task<int> CreateAsync(int issue, string description, int techerId);

        IEnumerable<T> GetAll<T>();

        T Grade<T>(int id);

        Grade GetGrade(int id);

        Task UpdateGrade(Grade grade);

        Task AddStudetToGrade(int gradeId, int studentId);

        Task DeleteGrade(Grade grade);
    }
}

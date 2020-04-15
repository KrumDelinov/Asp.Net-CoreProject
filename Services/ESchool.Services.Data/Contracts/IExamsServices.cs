using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IExamsServices
    {
        Task<int> CreateAsync(string axamType, decimal result, int teacherId);

        Task AddExamToStudent(int studentId, int examId);
    }
}

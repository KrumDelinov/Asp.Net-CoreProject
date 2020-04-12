using ESchool.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface IStudentsServices
    {

        Task<int> CreateAsync(string firstName, string lastName, DateTime birthDate, int gradeId);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        T Student<T>(int id);

        Student GetStudent(int id);
    }
}

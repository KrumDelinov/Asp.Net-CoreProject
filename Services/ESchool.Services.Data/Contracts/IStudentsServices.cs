namespace ESchool.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public interface IStudentsServices
    {
        Task<int> CreateAsync(string firstName, string lastName, DateTime birthDate, int gradeId);

        Task AddAttendanceToStudent(int studentId, int attndanceId);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllStudentAttendaces<T>(int studentId);

        IEnumerable<T> GetAllCourseStudents<T>(int courseId);

        IEnumerable<T> GetAllParentStudents<T>(int id);

        T Student<T>(int id);

        Student GetStudent(int id);
    }
}

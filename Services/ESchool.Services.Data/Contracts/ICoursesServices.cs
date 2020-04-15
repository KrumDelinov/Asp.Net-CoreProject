using ESchool.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESchool.Services.Data.Contracts
{
    public interface ICoursesServices
    {
        Task<int> CreateAsync(int issue, string description, int techerId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllTeacherCourses<T>(int id);

        T Course<T>(int id);

        Course GetCourse(int id);

        Task UpdateCourse(Course course);

        Task AddStudetToCourse(int courseId, int studentId);

        Task DeleteCourse(Course course);

        Task AddTeachersToCourse(int courseId, int techerId);
    }
}

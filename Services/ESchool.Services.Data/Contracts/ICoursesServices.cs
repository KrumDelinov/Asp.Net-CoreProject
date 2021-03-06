﻿namespace ESchool.Services.Data.Contracts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public interface ICoursesServices
    {
        Task<int> CreateAsync(int issue, string description, int techerId);

        IEnumerable<T> GetAll<T>();

        int GetCount();

        IEnumerable<T> GetAllTeacherCourses<T>(int id);

        IEnumerable<T> GetCourseAllTeachers<T>(int id);

        T Course<T>(int id);

        int GetTeacherCourseId(int teacherId);

        Course GetCourse(int id);

        Task UpdateCourse(Course course);

        Task AddStudetToCourse(int courseId, int studentId);

        Task DeleteCourse(Course course);

        Task AddTeachersToCourse(int courseId, int techerId);
    }
}

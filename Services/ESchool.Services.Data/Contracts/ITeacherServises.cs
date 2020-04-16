namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public interface ITeacherServises
    {
        Task<int> CreateAsync(string firstName, string lastName, int subjectId, string email = null);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        Task<int> SetClassroomToTeacher(int id);

        Task<int> SetGradeToTeacher(int id);

        Task<int> RemoveClassroomFromTeacher(int id);

        Task<int> RemoveGradeFromTeacher(int id);

        Teacher GetUserTeacher(string userId);

        Teacher GetTeacher(int id);

        Task UpdateTeacher(Teacher teacher);

        Task DeleteTeacher(Teacher teacher);

        T Teacher<T>(int id);
    }
}

namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public interface ITeacherServises
    {
        Task<int> CreateAsync(string firstName, string lastName, int subjectId);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        Task<int> SetClassroomToTeacher(int id);

        Teacher GetTeacher(int id);

        void UpdateTeacher(int id, string firstName, string lastName);

        T Teacher<T>(int id);
    }
}

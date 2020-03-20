namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ESchool.Data.Models;

    public interface ITeacherServises
    {
        Task<int> CreateAsync(string firstName, string lastName, string userId, int subjectId);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        Teacher Teacher(int id);
    }
}

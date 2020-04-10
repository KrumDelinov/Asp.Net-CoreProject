namespace ESchool.Services.Data
{
    using ESchool.Data.Models;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ISubjectsServices
    {
        Task<int> CreateAsync(string description);

        T Subject<T>(int id);

        int GetCount();

        IEnumerable<T> GetAll<T>();

        Subject GetSubject(int id);

        //Task UpdateTeacher(Teacher teacher);

        Task DeleteSubject(Subject subject);
    }
}

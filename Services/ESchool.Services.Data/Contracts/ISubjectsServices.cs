namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface ISubjectsServices
    {
        int GetCount();

        IEnumerable<T> GetAll<T>();
    }
}

namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IClassroomsServices
    {
        Task<int> CreateAsync(string number, int techerId);

        IEnumerable<T> GetAll<T>();
    }
}

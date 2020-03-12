namespace ESchool.Services.Data
{
    using System.Collections.Generic;
    using System.Text;

    public interface IClassroomServices
    {
        IEnumerable<T> GetAll<T>();
    }
}

namespace ESchool.Services.Data.Contracts
{
    using ESchool.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IParentServices
    {
        Task<int> CreateAsync(string firstName, string lastName, string email, string userId);

        int GetCount();

        int GetParentId(string email);

        bool HasParent(string email);

        bool ParentHasStudent(string email, int studentId);

        Task SetStudentToParent(int parentId, int studentId);

        Parent GetUserParent(string userId);

        T Parent<T>(int id);

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taskit_server.Services.Interfaces
{
    public interface IRepository<T>  
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        Task<long> Add(T entity);
    }
}

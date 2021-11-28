using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taskit_server.Model.Entities;

namespace Taskit_server.Services.Interfaces
{
    public interface IRepository<T>  where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
        ICollection<T> GetByIds(params int[] Ids);
        Task<T> Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}

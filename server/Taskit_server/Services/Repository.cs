using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taskit_server.Services.Interfaces;

namespace Taskit_server.Services
{
    public class Repository<T> : IRepository<T> 
    {
        public Repository(DataContext context)
        {

        }
        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<long> Add(T entity)
        {
            throw new NotImplementedException();
        }

    }
}

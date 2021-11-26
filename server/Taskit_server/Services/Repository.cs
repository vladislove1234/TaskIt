using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskit_server.Db;
using Taskit_server.Services.Interfaces;

namespace Taskit_server.Services
{
    public class Repository<T>  : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int Id)
        {
            return null;
        }

        public Task<long> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskit_server.Db;
using Taskit_server.Model.Entities;
using Taskit_server.Services.Interfaces;

namespace Taskit_server.Services
{
    public class Repository<T>  : IRepository<T> where T : BaseEntity
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
            var result = _context.Set<T>().FirstOrDefault(x => x.Id == Id);

            if (result == null)
            {
                //todo: need to add logger
                return null;
            }

            return result;
        }
        public void Update(T entity)
        {
            var element = GetById(entity.Id);
            element = entity;
            _context.Set<T>().Update(element);
            _context.SaveChanges();
        }

        public async Task<T> Add(T entity)
        {
            var result = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public void Remove(T entity)
        {
            var element = GetById(entity.Id);
            _context.Set<T>().Remove(element);
            _context.SaveChanges();
        }

        public ICollection<T> GetByIds(params int[] Ids)
        {
            var list = new List<T>();
            foreach(int Id in Ids)
            {
                var entity = GetById(Id);
                if (entity != null)
                    list.Add(entity);
            }
            return list;
        }
    }
}

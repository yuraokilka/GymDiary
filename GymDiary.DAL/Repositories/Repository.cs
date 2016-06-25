using GymDiary.DAL.EF;
using GymDiary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GymDiary.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext Context;
        protected DbSet<T> Entities
        {
            get { return Context.Set<T>(); }
        }

        public Repository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<T> Find(Func<T, Boolean> predicate)
        {
            return Entities.Where(predicate).ToList();
        }

        public IEnumerable<T> GetAll()
        {
            return Entities;
        }

        public T Get(int id)
        {
            return Entities.Find(id);
        }

        public void Create(T entity)
        {
            Entities.Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            Entities.Remove(entity);
            Context.SaveChanges();
        }
    }
}

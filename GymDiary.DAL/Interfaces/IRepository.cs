using System;
using System.Collections.Generic;

namespace GymDiary.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}

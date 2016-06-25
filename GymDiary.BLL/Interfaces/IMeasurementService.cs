using GymDiary.BLL.DTO;
using System;
using System.Collections.Generic;

namespace GymDiary.BLL.Interfaces
{
    public interface IMeasurementService<T> where T : class
    {
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<MeasurementDTO> GetAllSortedByDate(string userId);
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}

using GymDiary.BLL.DTO;
using GymDiary.DAL.Entities;
using System;
using System.Collections.Generic;

namespace GymDiary.BLL.Interfaces
{
    public interface IWorkoutService
    {
        IEnumerable<Workout> Find(Func<Workout, bool> predicate);
        IEnumerable<WorkoutDTO> GetByDate(string userId, DateTime from, DateTime to);
        WorkoutDTO Get(int id);
        void Create(Workout entity);
        void Update(Workout entity);
        void Delete(int id);
    }
}

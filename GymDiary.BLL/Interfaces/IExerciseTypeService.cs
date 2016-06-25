using GymDiary.BLL.DTO;
using GymDiary.DAL.Entities;
using System;
using System.Collections.Generic;

namespace GymDiary.BLL.Interfaces
{
    public interface IExerciseTypeService
    {
        IEnumerable<ExerciseType> Find(Func<ExerciseType, bool> predicate);
        IEnumerable<ExerciseTypeDTO> GetAll(string userId);
        IEnumerable<ExerciseTypeDTO> GetFavorites(string userId);
        ExerciseType Get(int id);
        void MarkAsFavorite(int id);
        void Create(ExerciseType entity);
        void Update(ExerciseType entity);
        void Delete(int id);
    }
}

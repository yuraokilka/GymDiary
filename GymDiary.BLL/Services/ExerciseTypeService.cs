using GymDiary.BLL.DTO;
using GymDiary.BLL.Interfaces;
using GymDiary.DAL.EF;
using GymDiary.DAL.Entities;
using GymDiary.DAL.Interfaces;
using GymDiary.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymDiary.BLL.Services
{
    public class ExerciseTypeService : IExerciseTypeService
    {
        private ApplicationDbContext context;
        private IRepository<ExerciseType> repository;

        public ExerciseTypeService(ApplicationDbContext db)
        {
            context = db;
            repository = new Repository<ExerciseType>(context);
        }

        public IEnumerable<ExerciseType> Find(Func<ExerciseType, bool> predicate)
        {
            return repository.GetAll().ToList().Where(predicate).ToList();
        }

        public IEnumerable<ExerciseTypeDTO> GetAll(string userId)
        {
            var exerciseTypes = Find(e => e.ApplicationUser.Id == userId).ToList();
            var viewModel = new List<ExerciseTypeDTO>();
            if (exerciseTypes == null)
            {
                return viewModel;
            }
            foreach (var e in exerciseTypes)
            {
                viewModel.Add(MapExerciseType(e));
            }
            return viewModel;
        }

        public IEnumerable<ExerciseTypeDTO> GetFavorites(string userId)
        {
            var exerciseTypes = Find(e => (e.ApplicationUser.Id == userId) && (e.IsFavorite == true)).ToList();
            var viewModel = new List<ExerciseTypeDTO>();
            foreach (var e in exerciseTypes)
            {
                viewModel.Add(MapExerciseType(e));
            }
            return viewModel;
        }

        public ExerciseType Get(int id)
        {
            return repository.Get(id);
        }

        public void MarkAsFavorite(int id)
        {
            var entity = repository.Get(id);
            entity.IsFavorite = !entity.IsFavorite;
            repository.Update(entity);
        }

        public void Create(ExerciseType entity)
        {
            repository.Create(entity);
        }

        public void Update(ExerciseType entity)
        {
            repository.Update(entity);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        private ExerciseTypeDTO MapExerciseType(ExerciseType model)
        {
            var modelDTO = new ExerciseTypeDTO
            {
                Id = model.Id,
                Name = model.Name,
                IsFavorite = model.IsFavorite,
                ApplicationUser = model.ApplicationUser
            };
            return modelDTO;
        }
    }
}

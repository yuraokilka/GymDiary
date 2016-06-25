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
    public class WorkoutService : IWorkoutService
    {
        private ApplicationDbContext context;
        private IRepository<Workout> workoutRepository;
        private IRepository<Exercise> exerciseRepository;
        private IRepository<Set> setRepository;

        public WorkoutService(ApplicationDbContext db)
        {
            context = db;
            workoutRepository = new Repository<Workout>(context);
            exerciseRepository = new Repository<Exercise>(context);
            setRepository = new Repository<Set>(context);
        }

        public IEnumerable<Workout> Find(Func<Workout, bool> predicate)
        {
            return workoutRepository.GetAll().ToList().Where(predicate).ToList();
        }
        
        public IEnumerable<WorkoutDTO> GetByDate(string userId, DateTime from, DateTime to)
        {
            // Get user`s workouts
            var workouts = Find(w => (w.ApplicationUser.Id == userId) &&
                                     (w.WorkoutDate >= from) &&
                                     (w.WorkoutDate <= to) )
                                     .ToList();
            // Sort by date descending (from new workouts to old ones)
            workouts.Sort((a, b) => b.WorkoutDate.CompareTo(a.WorkoutDate));
            // Map model to DTO
            var workoutDTOs = new List<WorkoutDTO>();
            foreach (var w in workouts)
            {
                workoutDTOs.Add(MapWorkout(w));
            }
            // Calculate total weight
            CalculateTotalWeight(ref workoutDTOs);
            return workoutDTOs;
        }

        public WorkoutDTO Get(int id)
        {
            var workoutDTO = workoutRepository.Get(id);
            if (workoutDTO == null)
            {
                return null;
            }
            return MapWorkout(workoutDTO);
        }

        public void Create(Workout workout)
        {
            workoutRepository.Create(workout);
        }

        public void Update(Workout workout)
        {
            workoutRepository.Delete(workout.Id);
            workoutRepository.Create(workout);
        }

        public void Delete(int id)
        {
            workoutRepository.Delete(id);
        }

        private WorkoutDTO MapWorkout(Workout model)
        {
            var modelDTO = new WorkoutDTO
            {
                Id = model.Id,
                WorkoutDate = model.WorkoutDate,
                Exercises = new List<Exercise>(model.Exercises),
                ApplicationUser = model.ApplicationUser
            };
            return modelDTO;
        }

        private void CalculateTotalWeight(ref List<WorkoutDTO> workoutDTOs)
        {
            if (workoutDTOs.Count == 0)
            {
                return;
            }
            double totalWeight = 0.0;
            foreach (var workout in workoutDTOs)
            {
                foreach (var exercise in workout.Exercises)
                {
                    foreach (var set in exercise.Sets)
                    {
                        totalWeight += set.Weight * set.Repetitions;
                    }
                }
                workout.TotalWeight = totalWeight;
                totalWeight = 0.0;
            }
        }

    }
}

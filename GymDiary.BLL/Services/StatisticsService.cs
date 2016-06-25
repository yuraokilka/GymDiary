using GymDiary.BLL.Interfaces;
using GymDiary.DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymDiary.BLL.Services
{
    public class StatisticsService : IStatisticsService
    {
        private ApplicationDbContext context;

        public StatisticsService(ApplicationDbContext db)
        {
            context = db;
        }

        public int CountWorkouts(string userId)
        {
            var workouts = context.Workouts.
                Select(w => new { Id = w.Id, uId = w.ApplicationUser.Id }).
                Where(w => w.uId == userId).
                ToList();
            if (workouts == null)
            {
                return 0;
            }
            return workouts.Count;
        }

        public int CountExercises(string userId)
        {
            var workouts = context.Workouts.
                Select(w => new { Id = w.Id, uId = w.ApplicationUser.Id }).
                Where(w => w.uId == userId).
                ToList();
            if (workouts == null)
            {
                return 0;
            }
            int sum = 0;
            foreach (var workout in workouts)
            {
                var exercises = context.Exercises.
                Select(e => new { Id = e.Id, wId = e.Workout.Id }).
                Where(e => e.wId == workout.Id).
                ToList();
                if (exercises == null)
                {
                    return sum;
                }
                sum += exercises.Count;
            }
            return sum;
        }

        public int CountSets(string userId)
        {
            var workouts = context.Workouts.
                Select(w => new { Id = w.Id, uId = w.ApplicationUser.Id }).
                Where(w => w.uId == userId).
                ToList();
            if (workouts == null)
            {
                return 0;
            }
            int sum = 0;
            foreach (var workout in workouts)
            {
                var exercises = context.Exercises.
                Select(e => new { Id = e.Id, wId = e.Workout.Id }).
                Where(e => e.wId == workout.Id).
                ToList();
                if (exercises == null)
                {
                    return sum;
                }
                foreach (var exercise in exercises)
                {
                    var sets = context.Sets.
                        Select(s => new { Id = s.Id, eId = s.Exercise.Id }).
                        Where(s => s.eId == exercise.Id).
                        ToList();
                    if (sets == null)
                    {
                        return sum;
                    }
                    sum += sets.Count;
                }
            }
            return sum;
        }

        public double CountTotalWeight(string userId)
        {
            var workouts = context.Workouts.
                Select(w => new { Id = w.Id, uId = w.ApplicationUser.Id }).
                Where(w => w.uId == userId).
                ToList();
            if (workouts == null)
            {
                return 0;
            }
            double sum = 0.0;
            foreach (var workout in workouts)
            {
                var exercises = context.Exercises.
                Select(e => new { Id = e.Id, wId = e.Workout.Id }).
                Where(e => e.wId == workout.Id).
                ToList();
                if (exercises == null)
                {
                    return sum;
                }
                foreach (var exercise in exercises)
                {
                    var sets = context.Sets.
                        Select(s => new { Id = s.Id, eId = s.Exercise.Id, Weight = s.Weight, Repetitions = s.Repetitions }).
                        Where(s => s.eId == exercise.Id).
                        ToList();
                    if (sets == null)
                    {
                        return sum;
                    }
                    foreach (var set in sets)
                    {
                        sum += set.Weight * set.Repetitions;
                    }
                }
            }
            return sum;
        }

        public Dictionary<DateTime, int> GetWorkoutsPerMonth(string userId)
        {
            var workoutsPerMonth = new Dictionary<DateTime, int>();
            for (int i = 0; i < 12; i++)
            {
                var month = DateTime.Today.AddMonths(i - 11);
                var value = CountWorkoutsInMonth(userId, month);
                workoutsPerMonth.Add(month, value);
            }
            return workoutsPerMonth;
        }

        public Dictionary<DateTime, double> GetTotalWeightDynamics(string userId)
        {
            var totalWeightDynamics = new Dictionary<DateTime, double>();
            for (int i = 0; i < 12; i++)
            {
                var month = DateTime.Today.AddMonths(i - 11);
                var value = CalculateTotalWeightInMonth(userId, month);
                totalWeightDynamics.Add(month, value);
            }
            return totalWeightDynamics;
        }

        public Dictionary<string, int> GetFavoriteExerciseTypes(string userId, int amount)
        {
            var favoriteExerciseTypes = new Dictionary<string, int>();
            var exerciseTypes = context.ExerciseTypes.
                Select(e => new { Id = e.Id, Name = e.Name, Exercises = e.Exercises, uId = e.ApplicationUser.Id }).
                Where(e => e.uId == userId).
                ToList();
            if (exerciseTypes == null)
            {
                return favoriteExerciseTypes;
            }
            // Sort by number of exercises descending
            exerciseTypes.Sort((a, b) => b.Exercises.Count.CompareTo(a.Exercises.Count));
            amount = (exerciseTypes.Count < amount) ? exerciseTypes.Count : amount;
            for (int i = 0; i < amount; i++)
            {
                var name = exerciseTypes[i].Name;
                var number = exerciseTypes[i].Exercises.Count;
                favoriteExerciseTypes.Add(name, number);
            }
            return favoriteExerciseTypes;
        }

        public Dictionary<DateTime, double> GetWeightMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.WeightMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetHeightMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.HeightMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetUpperArmMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.UpperArmMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetForeArmMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.ForeArmMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetNeckMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.NeckMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetChestMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.ChestMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetWaistMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.WaistMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetHipsMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.HipsMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetThighMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.ThighMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        public Dictionary<DateTime, double> GetCalfMeasurementDynamics(string userId, DateTime from, DateTime to)
        {
            var measurementDynamics = new Dictionary<DateTime, double>();
            var measurements = context.CalfMeasurements.
                Select(m => new { Date = m.MeasurementDate, Value = m.Value, uId = m.ApplicationUser.Id }).
                Where(m => (m.uId == userId) && (m.Date >= from) && (m.Date <= to)).
                ToList();
            if (measurements == null)
            {
                return measurementDynamics;
            }
            foreach (var wm in measurements)
            {
                measurementDynamics.Add(wm.Date, wm.Value);
            }
            return measurementDynamics;
        }

        private int CountWorkoutsInMonth(string userId, DateTime month)
        {
            var workouts = context.Workouts.
                Select(w => new { Id = w.Id, Date = w.WorkoutDate, uId = w.ApplicationUser.Id }).
                Where(w => w.uId == userId && w.Date.Month == month.Month).
                ToList();
            if (workouts == null)
            {
                return 0;
            }
            return workouts.Count;
        }

        private double CalculateTotalWeightInMonth(string userId, DateTime month)
        {
            var workouts = context.Workouts.
                Select(w => new { Id = w.Id, Date = w.WorkoutDate, uId = w.ApplicationUser.Id }).
                Where(w => w.uId == userId && w.Date.Month == month.Month).
                ToList();
            if (workouts == null)
            {
                return 0.0;
            }
            double totalWeight = 0.0;
            foreach (var workout in workouts)
            {
                var exercises = context.Exercises.
                Select(e => new { Id = e.Id, wId = e.Workout.Id }).
                Where(e => e.wId == workout.Id).
                ToList();
                if (exercises == null)
                {
                    return totalWeight;
                }
                foreach (var exercise in exercises)
                {
                    var sets = context.Sets.
                        Select(s => new { Id = s.Id, eId = s.Exercise.Id, Weight = s.Weight, Repetitions = s.Repetitions }).
                        Where(s => s.eId == exercise.Id).
                        ToList();
                    if (sets == null)
                    {
                        return totalWeight;
                    }
                    foreach (var set in sets)
                    {
                        totalWeight += set.Weight * set.Repetitions;
                    }
                }
            }
            return totalWeight;
        }

    }
}

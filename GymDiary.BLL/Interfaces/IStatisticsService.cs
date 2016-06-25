using System;
using System.Collections.Generic;

namespace GymDiary.BLL.Interfaces
{
    public interface IStatisticsService
    {
        int CountWorkouts(string userId);
        int CountExercises(string userId);
        int CountSets(string userId);
        double CountTotalWeight(string userId);
        Dictionary<DateTime, int> GetWorkoutsPerMonth(string userId);
        Dictionary<string, int> GetFavoriteExerciseTypes(string userId, int amount);
        Dictionary<DateTime, double> GetTotalWeightDynamics(string userId);
        Dictionary<DateTime, double> GetWeightMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetHeightMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetUpperArmMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetForeArmMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetNeckMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetChestMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetWaistMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetHipsMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetThighMeasurementDynamics(string userId, DateTime from, DateTime to);
        Dictionary<DateTime, double> GetCalfMeasurementDynamics(string userId, DateTime from, DateTime to);
    }
}

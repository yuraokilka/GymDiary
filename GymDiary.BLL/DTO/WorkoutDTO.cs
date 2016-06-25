using GymDiary.DAL.Entities;
using System;
using System.Collections.Generic;

namespace GymDiary.BLL.DTO
{
    public class WorkoutDTO
    {
        public int Id { get; set; }
        public DateTime WorkoutDate { get; set; }
        public double TotalWeight { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public WorkoutDTO()
        {
            Exercises = new List<Exercise>();
        }
    }
}

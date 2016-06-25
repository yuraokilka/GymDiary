using System;
using System.Collections.Generic;

namespace GymDiary.DAL.Entities
{
    public class Workout
    {
        public int Id { get; set; }
        public DateTime WorkoutDate { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public Workout()
        {
            Exercises = new List<Exercise>();
        }
    }
}

using System.Collections.Generic;

namespace GymDiary.DAL.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Set> Sets { get; set; }
        public virtual Workout Workout { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }

        public Exercise()
        {
            Sets = new List<Set>();
        }
    }
}

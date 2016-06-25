using System.Collections.Generic;

namespace GymDiary.DAL.Entities
{
    public class ExerciseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFavorite { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public ExerciseType()
        {
            Exercises = new List<Exercise>();
        }
    }
}

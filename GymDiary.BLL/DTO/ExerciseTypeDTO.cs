using GymDiary.DAL.Entities;

namespace GymDiary.BLL.DTO
{
    public class ExerciseTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFavorite { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}

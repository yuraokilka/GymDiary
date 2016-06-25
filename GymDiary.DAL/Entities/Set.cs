namespace GymDiary.DAL.Entities
{
    public class Set
    {
        public int Id { get; set; }
        public int Repetitions { get; set; }
        public double Weight { get; set; }

        public virtual Exercise Exercise { get; set; }
    }
}

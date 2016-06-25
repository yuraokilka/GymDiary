using GymDiary.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace GymDiary.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("GDConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseType>()
                .HasMany(e => e.Exercises)
                .WithRequired(e => e.ExerciseType)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Workout>()
                .HasMany(e => e.Exercises)
                .WithRequired(w => w.Workout)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Exercise>()
                .HasMany(s => s.Sets)
                .WithRequired(e => e.Exercise)
                .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Set> Sets { get; set; }

        public DbSet<WeightMeasurement> WeightMeasurements { get; set; }
        public DbSet<HeightMeasurement> HeightMeasurements { get; set; }
        public DbSet<UpperArmMeasurement> UpperArmMeasurements { get; set; }
        public DbSet<ForeArmMeasurement> ForeArmMeasurements { get; set; }
        public DbSet<NeckMeasurement> NeckMeasurements { get; set; }
        public DbSet<ChestMeasurement> ChestMeasurements { get; set; }
        public DbSet<WaistMeasurement> WaistMeasurements { get; set; }
        public DbSet<HipsMeasurement> HipsMeasurements { get; set; }
        public DbSet<ThighMeasurement> ThighMeasurements { get; set; }
        public DbSet<CalfMeasurement> CalfMeasurements { get; set; }

    }

}

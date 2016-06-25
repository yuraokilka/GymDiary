using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymDiary.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        public string TrainerName { get; set; }

        public virtual ICollection<ExerciseType> ExerciseTypes { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }

        public virtual ICollection<WeightMeasurement> WeightMeasurements { get; set; }
        public virtual ICollection<HeightMeasurement> HeightMeasurements { get; set; }
        public virtual ICollection<UpperArmMeasurement> UpperArmMeasurements { get; set; }
        public virtual ICollection<ForeArmMeasurement> ForeArmMeasurements { get; set; }
        public virtual ICollection<NeckMeasurement> NeckMeasurements { get; set; }
        public virtual ICollection<ChestMeasurement> ChestMeasurements { get; set; }
        public virtual ICollection<WaistMeasurement> WaistMeasurements { get; set; }
        public virtual ICollection<HipsMeasurement> HipsMeasurements { get; set; }
        public virtual ICollection<ThighMeasurement> ThighMeasurements { get; set; }
        public virtual ICollection<CalfMeasurement> CalfMeasurements { get; set; }

        public ApplicationUser()
        {
            ExerciseTypes = new List<ExerciseType>();

            Workouts = new List<Workout>();

            WeightMeasurements = new List<WeightMeasurement>();
            HeightMeasurements = new List<HeightMeasurement>();
            UpperArmMeasurements = new List<UpperArmMeasurement>();
            ForeArmMeasurements = new List<ForeArmMeasurement>();
            NeckMeasurements = new List<NeckMeasurement>();
            ChestMeasurements = new List<ChestMeasurement>();
            WaistMeasurements = new List<WaistMeasurement>();
            HipsMeasurements = new List<HipsMeasurement>();
            ThighMeasurements = new List<ThighMeasurement>();
            CalfMeasurements = new List<CalfMeasurement>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public enum Sex
    {
        Male,
        Female,
        Other
    };

}

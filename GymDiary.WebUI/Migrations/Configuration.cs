using GymDiary.BLL.Services;
using GymDiary.DAL.EF;
using GymDiary.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Xml;

namespace GymDiary.WebUI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //SeedUsers(context);
            //SeedExerciseTypes(context);
            //SeedMeasurements(context);
            //SeedWorkouts(context);
            //GenerateERDiagram();
        }

        private void SeedUsers(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore); // Dispose this!
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            // adding Roles
            var trainerRole = new IdentityRole { Name = "Trainer" };
            var userRole = new IdentityRole { Name = "User" };
            roleManager.Create(trainerRole);
            roleManager.Create(userRole);

            // adding Trainers
            var trainer = new ApplicationUser()
            {
                UserName = "andriy.hryfovych",
                Email = "andriy.hryfovych@gmail.com",
                Sex = (byte)Sex.Male,
                BirthDate = DateTime.Now,
                RegistrationDate = DateTime.Now
            };
            userManager.Create(trainer, "Password#123");
            userManager.AddToRole(trainer.Id, trainerRole.Name);
            userManager.AddToRole(trainer.Id, userRole.Name);

            // adding Users
            var applicationUsers = new List<ApplicationUser>(InitialData.ApplicationUsers);
            applicationUsers.ForEach(u => userManager.Create(u, "Password#123"));
            applicationUsers.ForEach(u => userManager.AddToRole(u.Id, userRole.Name));
        }

        private void SeedExerciseTypes(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
            var exerciseTypeService = new ExerciseTypeService(context);
            
            // adding ExerciseTypes
            var exerciseTypes = new List<ExerciseType>(InitialData.ExerciseTypes);
            var applicationUsers = new List<ApplicationUser>(userManager.Users);
            foreach (var u in applicationUsers)
            {
                foreach(var e in exerciseTypes)
                {
                    e.ApplicationUser = u;
                    exerciseTypeService.Create(e);
                }
            }
        }

        private void SeedWorkouts(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
            var exerciseTypeService = new ExerciseTypeService(context);
            var workoutService = new WorkoutService(context);
                                   
            // adding Workouts
            var applicationUsers = new List<ApplicationUser>(userManager.Users);
            foreach (var u in applicationUsers)
            {
                for (int i = 0; i < 90; i++)
                {
                    var workout = new Workout();
                    if (i % 2 == 0)
                    {
                        workout = GetPreparedWorkoutTemplate1(u, exerciseTypeService);
                    }
                    else
                    {
                        workout = GetPreparedWorkoutTemplate2(u, exerciseTypeService);
                    }
                    workout.WorkoutDate = DateTime.Today.AddDays(-(i * 4));
                    workoutService.Create(workout);
                }
            }
        }

        private void SeedMeasurements(ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new ApplicationUserManager(userStore);
            var weightMeasurementService = new WeightMeasurementService(context);
            var heightMeasurementService = new HeightMeasurementService(context);
            var upperArmMeasurementService = new UpperArmMeasurementService(context);
            var foreArmMeasurementService = new ForeArmMeasurementService(context);
            var neckMeasurementService = new NeckMeasurementService(context);
            var chestMeasurementService = new ChestMeasurementService(context);
            var waistMeasurementService = new WaistMeasurementService(context);
            var hipsMeasurementService = new HipsMeasurementService(context);
            var thighMeasurementService = new ThighMeasurementService(context);
            var calfMeasurementService = new CalfMeasurementService(context);
            
            // adding Measurements
            var weightMeasurements = new List<WeightMeasurement>(InitialData.WeightMeasurement);
            var heightMeasurements = new List<HeightMeasurement>(InitialData.HeightMeasurement);
            var upperArmMeasurements = new List<UpperArmMeasurement>(InitialData.UpperArmMeasurement);
            var foreArmMeasurements = new List<ForeArmMeasurement>(InitialData.ForeArmMeasurement);
            var neckMeasurements = new List<NeckMeasurement>(InitialData.NeckMeasurement);
            var chestMeasurements = new List<ChestMeasurement>(InitialData.ChestMeasurement);
            var waistMeasurements = new List<WaistMeasurement>(InitialData.WaistMeasurement);
            var hipsMeasurements = new List<HipsMeasurement>(InitialData.HipsMeasurement);
            var thighMeasurements = new List<ThighMeasurement>(InitialData.ThighMeasurement);
            var calfMeasurements = new List<CalfMeasurement>(InitialData.CalfMeasurement);
            var applicationUsers = new List<ApplicationUser>(userManager.Users);
            foreach (var u in applicationUsers)
            {
                foreach (var m in weightMeasurements)
                {
                    m.ApplicationUser = u; // bind to user
                    weightMeasurementService.Create(m); // add to database
                }
                foreach (var m in heightMeasurements)
                {
                    m.ApplicationUser = u;
                    heightMeasurementService.Create(m);
                }
                foreach (var m in upperArmMeasurements)
                {
                    m.ApplicationUser = u;
                    upperArmMeasurementService.Create(m);
                }
                foreach (var m in foreArmMeasurements)
                {
                    m.ApplicationUser = u;
                    foreArmMeasurementService.Create(m);
                }
                foreach (var m in neckMeasurements)
                {
                    m.ApplicationUser = u;
                    neckMeasurementService.Create(m);
                }
                foreach (var m in chestMeasurements)
                {
                    m.ApplicationUser = u;
                    chestMeasurementService.Create(m);
                }
                foreach (var m in waistMeasurements)
                {
                    m.ApplicationUser = u;
                    waistMeasurementService.Create(m);
                }
                foreach (var m in hipsMeasurements)
                {
                    m.ApplicationUser = u;
                    hipsMeasurementService.Create(m);
                }
                foreach (var m in thighMeasurements)
                {
                    m.ApplicationUser = u;
                    thighMeasurementService.Create(m);
                }
                foreach (var m in calfMeasurements)
                {
                    m.ApplicationUser = u;
                    calfMeasurementService.Create(m);
                }
            }
        }

        private Workout GetPreparedWorkoutTemplate1(ApplicationUser user, ExerciseTypeService ets)
        {
            var workout = new Workout();
            workout.ApplicationUser = user;
            workout.WorkoutDate = DateTime.Today;
            workout.Exercises = new List<Exercise>();
            for (int j = 0; j < 5; j++)
            {
                var exercise = new Exercise();
                exercise.ExerciseType = ets.Find(
                    e => e.ApplicationUser == user &&
                    e.Name == InitialData.Exercises[j].Comment).
                    ToList().FirstOrDefault();
                //exercise.Workout = workout;
                exercise.Sets = new List<Set>();
                foreach (var s in InitialData.Exercises[j].Sets)
                {
                    exercise.Sets.Add(new Set
                    {
                        Weight = s.Weight,
                        Repetitions = s.Repetitions
                        //Exercise = exercise
                    });
                }
                workout.Exercises.Add(exercise);
            }
            workout.Exercises.ToList()[0].Comment = "Не ставити руки надто вузько";
            workout.Exercises.ToList()[1].Comment = "Не торкатися голови";
            workout.Exercises.ToList()[2].Comment = "Не забути пояс";
            workout.Exercises.ToList()[3].Comment = "Слідкувати за спиною";
            workout.Exercises.ToList()[4].Comment = "";
            return workout;
        }

        private Workout GetPreparedWorkoutTemplate2(ApplicationUser user, ExerciseTypeService ets)
        {
            var workout = new Workout();
            workout.ApplicationUser = user;
            workout.WorkoutDate = DateTime.Today;
            workout.Exercises = new List<Exercise>();
            for (int j = 0; j < 6; j++)
            {
                var exercise = new Exercise();
                exercise.ExerciseType = ets.Find(
                    e => e.ApplicationUser == user &&
                    e.Name == InitialData.Exercises[j + 4].Comment).
                    ToList().FirstOrDefault();
                //exercise.Workout = workout;
                exercise.Sets = new List<Set>();
                foreach (var s in InitialData.Exercises[j + 4].Sets)
                {
                    exercise.Sets.Add(new Set
                    {
                        Weight = s.Weight,
                        Repetitions = s.Repetitions
                        //Exercise = exercise
                    });
                }
                workout.Exercises.Add(exercise);
            }
            workout.Exercises.ToList()[0].Comment = "";
            workout.Exercises.ToList()[1].Comment = "Зменшити амплітуду руху";
            workout.Exercises.ToList()[2].Comment = "Спробувати підвісити додаткову вагу";
            workout.Exercises.ToList()[3].Comment = "";
            workout.Exercises.ToList()[4].Comment = "";
            workout.Exercises.ToList()[5].Comment = "";
            return workout;
        }

        private void GenerateERDiagram()
        {
            using (var context = new ApplicationDbContext())
            {
                using (var writer = new XmlTextWriter(@"C:\My Files\Model.edmx", Encoding.Default))
                {
                    EdmxWriter.WriteEdmx(context, writer);
                }
            }
        }

    }
}

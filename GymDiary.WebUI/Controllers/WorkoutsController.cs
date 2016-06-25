using GymDiary.BLL.DTO;
using GymDiary.BLL.Services;
using GymDiary.DAL.EF;
using GymDiary.DAL.Entities;
using GymDiary.WebUI.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymDiary.WebUI.Controllers
{
    [Authorize(Roles = "Trainer, User")]
    [RequireHttps]
    public class WorkoutsController : Controller
    {
        private ApplicationDbContext db;
        private UserStore<ApplicationUser> userStore;
        private ApplicationUserManager userManager;
        private ExerciseTypeService exerciseTypeService;
        private WorkoutService workoutService;
        
        public WorkoutsController()
        {
            db = new ApplicationDbContext();
            userStore = new UserStore<ApplicationUser>(db);
            userManager = new ApplicationUserManager(userStore);
            exerciseTypeService = new ExerciseTypeService(db);
            workoutService = new WorkoutService(db);
        }

        // GET: Workouts/All
        public async Task<ActionResult> All()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var workoutsDTO = workoutService.GetByDate(user.Id, DateTime.Today.AddMonths(-1), DateTime.Today);
            var workoutsVM = new List<WorkoutViewModel>();
            foreach (var w in workoutsDTO)
            {
                workoutsVM.Add(MapWorkoutDTO(w));
            }
            return View(workoutsVM);
        }

        // GET: Workouts/GetByDate
        public async Task<ActionResult> GetByDate(FilterWorkoutList constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var workoutsVM = new List<WorkoutViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                var workoutsDTO = workoutService.GetByDate(user.Id, constraints.From, constraints.To);
                if (workoutsDTO.ToList().Count != 0)
                {
                    foreach (var w in workoutsDTO)
                    {
                        workoutsVM.Add(MapWorkoutDTO(w));
                    }
                    return PartialView("_WorkoutListPartial", workoutsVM);
                }
                return PartialView("_NoResultsPartial");
            }
            return PartialView("_NoResultsPartial");
        }

        // GET: Workouts/Create
        public async Task<ActionResult> Create()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var exerciseTypeOptions = exerciseTypeService.GetAll(user.Id).ToList();
            if (exerciseTypeOptions.Count == 0)
            {
                return Redirect("/Exercises/All");
            }
            ViewBag.ExerciseTypeOptions = exerciseTypeOptions;

            var workoutVM = new WorkoutViewModel();
            workoutVM.WorkoutDate = DateTime.Today;
            workoutVM.Exercises.Add(new ExerciseViewModel());
            workoutVM.Exercises.First().Sets.Add(new SetViewModel());
            workoutVM.Exercises.First().Sets.Add(new SetViewModel());
            workoutVM.Exercises.First().Sets.Add(new SetViewModel());

            return View(workoutVM);
        }

        // POST: Workouts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkoutViewModel model)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var exerciseTypeOptions = exerciseTypeService.GetAll(user.Id).ToList();
            if (exerciseTypeOptions.Count == 0)
            {
                return Redirect("/Exercises/All");
            }
            ViewBag.ExerciseTypeOptions = exerciseTypeOptions;
            if (ModelState.IsValid)
            {
                var workout = MapWorkoutViewModel(model);
                
                var oldWorkout = workoutService.Find(w =>
                    w.ApplicationUser.Id == user.Id &&
                    w.WorkoutDate == workout.WorkoutDate).ToList();
                if (oldWorkout.Count == 0)
                {
                    foreach (var exercise in workout.Exercises)
                    {
                        exercise.ExerciseType = exerciseTypeService.Get(exercise.ExerciseType.Id);
                    }
                    workout.ApplicationUser = user;
                    workoutService.Create(workout);
                    return Redirect("/Workouts/All");
                }
                else
                {
                    ViewBag.Status = "A workout with such date already exists";
                    return View(model);
                }
            }
            return View(model);
        }

        // GET: Workouts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var exerciseTypeOptions = exerciseTypeService.GetAll(user.Id).ToList();
            if (exerciseTypeOptions.Count == 0)
            {
                return Redirect("/Exercises/All");
            }
            ViewBag.ExerciseTypeOptions = exerciseTypeOptions;

            var workoutDTO = workoutService.Get(id);
            if (workoutDTO == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (workoutDTO.ApplicationUser.Id != user.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var workoutVM = MapWorkoutDTO(workoutDTO);

            return View(workoutVM);
        }

        // POST: Workouts/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkoutViewModel model)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var exerciseTypeOptions = exerciseTypeService.GetAll(user.Id).ToList();
            if (exerciseTypeOptions.Count == 0)
            {
                return Redirect("/Exercises/All");
            }
            ViewBag.ExerciseTypeOptions = exerciseTypeOptions;
            if (ModelState.IsValid)
            {
                var workout = MapWorkoutViewModel(model);

                var oldWorkout = workoutService.Get(workout.Id);
                if (oldWorkout == null ||
                    oldWorkout.ApplicationUser.Id != user.Id)
                {
                    return View(model);
                }
                if (workout.WorkoutDate != oldWorkout.WorkoutDate)
                {
                    var sameDateWorkout = workoutService.Find(w =>
                    w.ApplicationUser.Id == user.Id &&
                    w.WorkoutDate == workout.WorkoutDate).ToList();
                    if (sameDateWorkout.Count != 0)
                    {
                        ViewBag.Status = "A workout with such date already exists";
                        return View(model);
                    }
                }
                foreach (var exercise in workout.Exercises)
                {
                    exercise.ExerciseType = exerciseTypeService.Get(exercise.ExerciseType.Id);
                }
                workout.ApplicationUser = user;
                workoutService.Update(workout);
                return Redirect("/Workouts/All");
            }
            return View(model);
        }

        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var workout = workoutService.Get(id);
            if (workout == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (workout.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            workoutService.Delete(id);
            return Json("Deleted successfully");
        }

        // GET: Workouts/AddExerciseEdit
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddExerciseEdit(int id = 1)
        {
            var exerciseTypeOptions = GetExerciseTypeOptions();
            if (exerciseTypeOptions.Count == 0)
            {
                return Redirect("/Exercises/All");
            }
            ViewBag.ExerciseTypeOptions = exerciseTypeOptions;

            var workoutVM = new WorkoutViewModel();
            workoutVM.Exercises.Add(new ExerciseViewModel());
            id = (id < 1) ? 1 : id;
            for (int i = 0; i < id; i++)
            {
                workoutVM.Exercises.First().Sets.Add(new SetViewModel());
            }
            return View(workoutVM);
        }
        
        private List<ExerciseTypeDTO> GetExerciseTypeOptions()
        {
            var user = userManager.FindById(User.Identity.GetUserId());
            if (user == null)
            {
                return new List<ExerciseTypeDTO>();
            }
            return exerciseTypeService.GetAll(user.Id).ToList();
        }

        private WorkoutViewModel MapWorkoutDTO(WorkoutDTO model)
        {
            var exercises = new List<ExerciseViewModel>();
            foreach (var exercise in model.Exercises)
            {
                exercises.Add(MapExercise(exercise));
            }
            var viewModel = new WorkoutViewModel
            {
                Id = model.Id,
                WorkoutDate = model.WorkoutDate,
                Exercises = exercises,
                TotalWeight = String.Format("{0:0.##}", Math.Round(model.TotalWeight, 2))
            };
            return viewModel;
        }

        private ExerciseViewModel MapExercise(Exercise model)
        {
            var sets = new List<SetViewModel>();
            foreach (var set in model.Sets)
            {
                sets.Add(MapSet(set));
            }
            var viewModel = new ExerciseViewModel
            {
                Id = model.Id,
                Comment = model.Comment,
                ExerciseType = model.ExerciseType,
                Sets = sets
            };
            return viewModel;
        }

        private SetViewModel MapSet(Set model)
        {
            var viewModel = new SetViewModel
            {
                Id = model.Id,
                Repetitions = Convert.ToString(model.Repetitions),
                Weight = String.Format("{0:0.##}", Math.Round(model.Weight, 2))
            };
            return viewModel;
        }

        private Workout MapWorkoutViewModel(WorkoutViewModel model)
        {
            var exercises = new List<Exercise>();
            foreach (var exercise in model.Exercises)
            {
                exercises.Add(MapExerciseViewModel(exercise));
            }
            var workout = new Workout
            {
                Id = model.Id,
                WorkoutDate = model.WorkoutDate,
                Exercises = exercises
            };
            return workout;
        }

        private Exercise MapExerciseViewModel(ExerciseViewModel model)
        {
            var sets = new List<Set>();
            foreach (var set in model.Sets)
            {
                sets.Add(MapSetViewModel(set));
            }
            var exercise = new Exercise
            {
                Id = model.Id,
                Comment = model.Comment,
                ExerciseType = model.ExerciseType,
                Sets = sets
            };
            return exercise;
        }

        private Set MapSetViewModel(SetViewModel model)
        {
            var set = new Set
            {
                Id = model.Id,
                Repetitions = Convert.ToInt32(model.Repetitions),
                Weight = Convert.ToDouble(model.Weight)
            };
            return set;
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
                if (userStore != null)
                {
                    userStore.Dispose();
                    userStore = null;
                }
                if (userManager != null)
                {
                    userManager.Dispose();
                    userManager = null;
                }
            }
            base.Dispose(disposing);
        }

    }
}
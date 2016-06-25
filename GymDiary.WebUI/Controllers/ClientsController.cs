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
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymDiary.WebUI.Controllers
{
    [Authorize(Roles = "Trainer")]
    [RequireHttps]
    public class ClientsController : Controller
    {
        private ApplicationDbContext db;
        private UserStore<ApplicationUser> userStore;
        private ApplicationUserManager userManager;
        private WorkoutService workoutService;

        public ClientsController()
        {
            db = new ApplicationDbContext();
            userStore = new UserStore<ApplicationUser>(db);
            userManager = new ApplicationUserManager(userStore);
            workoutService = new WorkoutService(db);
        }

        // GET: Clients
        public ActionResult Index()
        {
            var workoutsVM = new List<WorkoutViewModel>();            
            return View(workoutsVM);
        }

        // GET: Clients/GetByDate
        public async Task<ActionResult> GetByDate(FilterClientsWorkoutList constraints)
        {
            var workoutsVM = new List<WorkoutViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                var trainer = await userManager.FindByIdAsync(User.Identity.GetUserId());
                var client = await userManager.FindByNameAsync(constraints.Username);
                if (trainer != null && client != null && trainer.UserName == client.TrainerName)
                {
                    var workoutsDTO = workoutService.GetByDate(client.Id, constraints.From, constraints.To);
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
            return PartialView("_NoResultsPartial");
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
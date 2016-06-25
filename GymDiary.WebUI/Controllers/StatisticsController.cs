using GymDiary.BLL.Services;
using GymDiary.DAL.EF;
using GymDiary.DAL.Entities;
using GymDiary.WebUI.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymDiary.WebUI.Controllers
{
    [Authorize(Roles = "Trainer, User")]
    [RequireHttps]
    public class StatisticsController : Controller
    {
        private ApplicationDbContext db;
        private UserStore<ApplicationUser> userStore;
        private ApplicationUserManager userManager;
        private StatisticsService statisticsService;

        public StatisticsController()
        {
            db = new ApplicationDbContext();
            userStore = new UserStore<ApplicationUser>(db);
            userManager = new ApplicationUserManager(userStore);
            statisticsService = new StatisticsService(db);
        }

        // GET: Statistics/All
        public async Task<ActionResult> All()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            AllStatisticsViewModel statisticsVM = new AllStatisticsViewModel();
            statisticsVM.TotalStatistics.WorkoutsNum = statisticsService.CountWorkouts(user.Id);
            statisticsVM.TotalStatistics.ExercisesNum = statisticsService.CountExercises(user.Id);
            statisticsVM.TotalStatistics.SetsNum = statisticsService.CountSets(user.Id);
            statisticsVM.TotalStatistics.TotalWeight = statisticsService.CountTotalWeight(user.Id);
            statisticsVM.FavoriteExerciseTypes = statisticsService.GetFavoriteExerciseTypes(user.Id, 5);
            return View(statisticsVM);
        }

        // GET: Statistics/GetWorkoutsPerMonth
        public async Task<ActionResult> GetWorkoutsPerMonth()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var workoutsPerMonth = statisticsService.GetWorkoutsPerMonth(user.Id);
            var workoutsPerMonthVM = new List<ChartPointViewModel>();
            foreach (var wpm in workoutsPerMonth)
            {
                workoutsPerMonthVM.Add(
                    new ChartPointViewModel()
                    {
                        Date = wpm.Key.ToString("yyyy-MM"),
                        Value = wpm.Value
                    });
            }
            return Json(workoutsPerMonthVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetTotalWeightDynamics
        public async Task<ActionResult> GetTotalWeightDynamics()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var totalWeightDynamics = statisticsService.GetTotalWeightDynamics(user.Id);
            var totalWeightDynamicsVM = new List<ChartPointViewModel>();
            foreach (var twd in totalWeightDynamics)
            {
                totalWeightDynamicsVM.Add(
                    new ChartPointViewModel()
                    {
                        Date = twd.Key.ToString("yyyy-MM"),
                        Value = Math.Round(twd.Value, 1)
                    });
            }
            return Json(totalWeightDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetWeightMeasurementDynamics
        public async Task<ActionResult> GetWeightMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetWeightMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetHeightMeasurementDynamics
        public async Task<ActionResult> GetHeightMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetHeightMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetUpperArmMeasurementDynamics
        public async Task<ActionResult> GetUpperArmMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetUpperArmMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetForeArmMeasurementDynamics
        public async Task<ActionResult> GetForeArmMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetForeArmMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetNeckMeasurementDynamics
        public async Task<ActionResult> GetNeckMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetNeckMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetChestMeasurementDynamics
        public async Task<ActionResult> GetChestMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetChestMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetWaistMeasurementDynamics
        public async Task<ActionResult> GetWaistMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetWaistMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetHipsMeasurementDynamics
        public async Task<ActionResult> GetHipsMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetHipsMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetThighMeasurementDynamics
        public async Task<ActionResult> GetThighMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetThighMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
        }

        // GET: Statistics/GetCalfMeasurementDynamics
        public async Task<ActionResult> GetCalfMeasurementDynamics(FilterMeasurementDynamics constraints)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            var measurementDynamics = statisticsService.
                GetCalfMeasurementDynamics(user.Id, constraints.From, constraints.To);
            var measurementDynamicsVM = new List<ChartPointViewModel>();
            if (ModelState.IsValid && (constraints.From < constraints.To))
            {
                foreach (var md in measurementDynamics)
                {
                    measurementDynamicsVM.Add(
                        new ChartPointViewModel()
                        {
                            Date = md.Key.ToString("yyyy-MM-dd"),
                            Value = Math.Round(md.Value, 1)
                        });
                }
            }
            return Json(measurementDynamicsVM, JsonRequestBehavior.AllowGet);
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
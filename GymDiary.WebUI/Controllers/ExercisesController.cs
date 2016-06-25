using GymDiary.BLL.DTO;
using GymDiary.BLL.Services;
using GymDiary.DAL.EF;
using GymDiary.DAL.Entities;
using GymDiary.WebUI.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace GymDiary.WebUI.Controllers
{
    [Authorize(Roles = "Trainer, User")]
    [RequireHttps]
    public class ExercisesController : Controller
    {
        private ApplicationDbContext db;
        private UserStore<ApplicationUser> userStore;
        private ApplicationUserManager userManager;
        private ExerciseTypeService exerciseTypeService;

        public ExercisesController()
        {
            db = new ApplicationDbContext();
            userStore = new UserStore<ApplicationUser>(db);
            userManager = new ApplicationUserManager(userStore);
            exerciseTypeService = new ExerciseTypeService(db);
        }

        // GET: Exercises/All
        public async Task<ActionResult> All()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var allExerciseTypesDTO = exerciseTypeService.GetAll(user.Id);
            var allExerciseTypesVM = new List<ExerciseTypeListViewModel>();
            foreach (var e in allExerciseTypesDTO)
            {
                allExerciseTypesVM.Add(MapExerciseTypeDTO(e));
            }
            return View(allExerciseTypesVM);
        }

        // GET: Exercises/GetAll
        public async Task<ActionResult> GetAll()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var allExerciseTypesDTO = exerciseTypeService.GetAll(user.Id);
            var viewModel = new List<ExerciseTypeListViewModel>();
            foreach (var e in allExerciseTypesDTO)
            {
                viewModel.Add(MapExerciseTypeDTO(e));
            }
            ViewBag.Table = "All";
            return PartialView("_TablePartial", viewModel);
        }

        // GET: Exercises/GetFavorites
        public async Task<ActionResult> GetFavorites()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var favExerciseTypesDTO = exerciseTypeService.GetFavorites(user.Id);
            var viewModel = new List<ExerciseTypeListViewModel>();
            foreach (var e in favExerciseTypesDTO)
            {
                viewModel.Add(MapExerciseTypeDTO(e));
            }
            ViewBag.Table = "Favorites";
            return PartialView("_TablePartial", viewModel);
        }

        // GET: Exercises/GetDetails/5
        public async Task<ActionResult> GetDetails(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var exercises = exerciseTypeService.Find(e =>
                    e.ApplicationUser.Id == user.Id &&
                    e.Id == id).ToList();
            if (exercises.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            var viewModel = new EditExerciseTypeViewModel()
            {
                Id = exercises.FirstOrDefault().Id,
                Name = exercises.FirstOrDefault().Name,
                Description = exercises.FirstOrDefault().Description,
                IsFavorite = exercises.FirstOrDefault().IsFavorite
            };
            return PartialView("_EditPartial", viewModel);
        }

        // POST: Exercises/Create
        [HttpPost]
        public async Task<JsonResult> Create(CreateExerciseTypeViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var exercise = new ExerciseType
                {
                    Name = viewModel.Name
                };
                var oldExercise = exerciseTypeService.Find(e =>
                    e.ApplicationUser.Id == user.Id &&
                    e.Name == exercise.Name).ToList();
                if (oldExercise.Count == 0)
                {
                    exercise.ApplicationUser = user;
                    exerciseTypeService.Create(exercise);
                    return Json("Created successfully");
                }
                else
                {
                    return Json("An exercise with such name already exists");
                }
            }
            string errors = "";
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors += error.ErrorMessage + ". ";
                }
            }
            // error
            return Json(errors);
        }

        // POST: Exercises/Update
        [HttpPost]
        public async Task<JsonResult> Update(EditExerciseTypeViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var oldExercise = exerciseTypeService.Find(e =>
                    e.ApplicationUser.Id == user.Id &&
                    e.Id == viewModel.Id).ToList();
                if (oldExercise.Count == 0)
                {
                    return Json("Updated unsuccessfully");
                }
                else
                {
                    oldExercise.First().Name = viewModel.Name;
                    oldExercise.First().Description = viewModel.Description;
                    oldExercise.First().IsFavorite = viewModel.IsFavorite;
                    exerciseTypeService.Update(oldExercise.First());
                    return Json("Updated successfully");
                }
            }
            string errors = "";
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    errors += error.ErrorMessage + ". ";
                }
            }
            // error
            return Json(errors);
        }

        // POST: Exercises/MarkAsFavorite/5
        [HttpPost, ActionName("MarkAsFavorite")]
        public async Task<JsonResult> MarkAsFavoriteConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var exercise = exerciseTypeService.Get(id);
            if (exercise == null)
            {
                return Json("Not marked as favorite");
            }
            if (exercise.ApplicationUser.Id != user.Id)
            {
                return Json("Not marked as favorite");
            }
            exerciseTypeService.MarkAsFavorite(id);
            return Json("Marked as favorite successfully");
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var exercise = exerciseTypeService.Get(id);
            if (exercise == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (exercise.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            exerciseTypeService.Delete(id);
            return Json("Deleted successfully");
        }

        private ExerciseTypeListViewModel MapExerciseTypeDTO(ExerciseTypeDTO model)
        {
            var viewModel = new ExerciseTypeListViewModel
            {
                Id = model.Id,
                Name = model.Name,
                IsFavorite = model.IsFavorite,
                ApplicationUser = model.ApplicationUser
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
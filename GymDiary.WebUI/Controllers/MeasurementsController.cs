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
    public class MeasurementsController : Controller
    {
        private ApplicationDbContext db;
        private UserStore<ApplicationUser> userStore;
        private ApplicationUserManager userManager;
        private WeightMeasurementService weightMeasurementService;
        private HeightMeasurementService heightMeasurementService;
        private UpperArmMeasurementService upperArmMeasurementService;
        private ForeArmMeasurementService foreArmMeasurementService;
        private NeckMeasurementService neckMeasurementService;
        private ChestMeasurementService chestMeasurementService;
        private WaistMeasurementService waistMeasurementService;
        private HipsMeasurementService hipsMeasurementService;
        private ThighMeasurementService thighMeasurementService;
        private CalfMeasurementService calfMeasurementService;

        public MeasurementsController()
        {
            db = new ApplicationDbContext();
            userStore = new UserStore<ApplicationUser>(db);
            userManager = new ApplicationUserManager(userStore);
            weightMeasurementService = new WeightMeasurementService(db);
            heightMeasurementService = new HeightMeasurementService(db);
            upperArmMeasurementService = new UpperArmMeasurementService(db);
            foreArmMeasurementService = new ForeArmMeasurementService(db);
            neckMeasurementService = new NeckMeasurementService(db);
            chestMeasurementService = new ChestMeasurementService(db);
            waistMeasurementService = new WaistMeasurementService(db);
            hipsMeasurementService = new HipsMeasurementService(db);
            thighMeasurementService = new ThighMeasurementService(db);
            calfMeasurementService = new CalfMeasurementService(db);
        }

        // GET: Measurements/All
        public async Task<ActionResult> All()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var weightDTO = weightMeasurementService.GetAllSortedByDate(user.Id);
            var heightDTO = heightMeasurementService.GetAllSortedByDate(user.Id);
            var upperArmDTO = upperArmMeasurementService.GetAllSortedByDate(user.Id);
            var foreArmDTO = foreArmMeasurementService.GetAllSortedByDate(user.Id);
            var neckDTO = neckMeasurementService.GetAllSortedByDate(user.Id);
            var chestDTO = chestMeasurementService.GetAllSortedByDate(user.Id);
            var waistDTO = waistMeasurementService.GetAllSortedByDate(user.Id);
            var hipsDTO = hipsMeasurementService.GetAllSortedByDate(user.Id);
            var thighDTO = thighMeasurementService.GetAllSortedByDate(user.Id);
            var calfDTO = calfMeasurementService.GetAllSortedByDate(user.Id);

            var weightViewModel = new List<MeasurementViewModel>();
            var heightViewModel = new List<MeasurementViewModel>();
            var upperArmViewModel = new List<MeasurementViewModel>();
            var foreArmViewModel = new List<MeasurementViewModel>();
            var neckViewModel = new List<MeasurementViewModel>();
            var chestViewModel = new List<MeasurementViewModel>();
            var waistViewModel = new List<MeasurementViewModel>();
            var hipsViewModel = new List<MeasurementViewModel>();
            var thighViewModel = new List<MeasurementViewModel>();
            var calfViewModel = new List<MeasurementViewModel>();

            foreach (var m in weightDTO)
            {
                weightViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in heightDTO)
            {
                heightViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in upperArmDTO)
            {
                upperArmViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in foreArmDTO)
            {
                foreArmViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in neckDTO)
            {
                neckViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in chestDTO)
            {
                chestViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in waistDTO)
            {
                waistViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in hipsDTO)
            {
                hipsViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in thighDTO)
            {
                thighViewModel.Add(MapMeasurementDTO(m));
            }
            foreach (var m in calfDTO)
            {
                calfViewModel.Add(MapMeasurementDTO(m));
            }

            var allMeasurementsViewModel = new AllMeasurementsViewModel
            {
                weightMeasurements = weightViewModel,
                heightMeasurements = heightViewModel,
                upperArmMeasurements = upperArmViewModel,
                foreArmMeasurements = foreArmViewModel,
                neckMeasurements = neckViewModel,
                chestMeasurements = chestViewModel,
                waistMeasurements = waistViewModel,
                hipsMeasurements = hipsViewModel,
                thighMeasurements = thighViewModel,
                calfMeasurements = calfViewModel,
            };
            return View(allMeasurementsViewModel);
        }

        // GET: Measurements/GetAllWeight
        public async Task<ActionResult> GetAllWeight()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var weightDTO = weightMeasurementService.GetAllSortedByDate(user.Id);
            var weightViewModel = new List<MeasurementViewModel>();
            foreach (var m in weightDTO)
            {
                weightViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Weight";
            return PartialView("_TablePartial", weightViewModel);
        }

        // GET: Measurements/GetAllHeight
        public async Task<ActionResult> GetAllHeight()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var heightDTO = heightMeasurementService.GetAllSortedByDate(user.Id);
            var heightViewModel = new List<MeasurementViewModel>();
            foreach (var m in heightDTO)
            {
                heightViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Height";
            return PartialView("_TablePartial", heightViewModel);
        }

        // GET: Measurements/GetAllUpperArm
        public async Task<ActionResult> GetAllUpperArm()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var upperArmDTO = upperArmMeasurementService.GetAllSortedByDate(user.Id);
            var upperArmViewModel = new List<MeasurementViewModel>();
            foreach (var m in upperArmDTO)
            {
                upperArmViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "UpperArm";
            return PartialView("_TablePartial", upperArmViewModel);
        }

        // GET: Measurements/GetAllForeArm
        public async Task<ActionResult> GetAllForeArm()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var foreArmDTO = foreArmMeasurementService.GetAllSortedByDate(user.Id);
            var foreArmViewModel = new List<MeasurementViewModel>();
            foreach (var m in foreArmDTO)
            {
                foreArmViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "ForeArm";
            return PartialView("_TablePartial", foreArmViewModel);
        }

        // GET: Measurements/GetAllNeck
        public async Task<ActionResult> GetAllNeck()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var neckDTO = neckMeasurementService.GetAllSortedByDate(user.Id);
            var neckViewModel = new List<MeasurementViewModel>();
            foreach (var m in neckDTO)
            {
                neckViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Neck";
            return PartialView("_TablePartial", neckViewModel);
        }

        // GET: Measurements/GetAllChest
        public async Task<ActionResult> GetAllChest()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var chestDTO = chestMeasurementService.GetAllSortedByDate(user.Id);
            var chestViewModel = new List<MeasurementViewModel>();
            foreach (var m in chestDTO)
            {
                chestViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Chest";
            return PartialView("_TablePartial", chestViewModel);
        }

        // GET: Measurements/GetAllWaist
        public async Task<ActionResult> GetAllWaist()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var waistDTO = waistMeasurementService.GetAllSortedByDate(user.Id);
            var waistViewModel = new List<MeasurementViewModel>();
            foreach (var m in waistDTO)
            {
                waistViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Waist";
            return PartialView("_TablePartial", waistViewModel);
        }

        // GET: Measurements/GetAllHips
        public async Task<ActionResult> GetAllHips()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var hipsDTO = hipsMeasurementService.GetAllSortedByDate(user.Id);
            var hipsViewModel = new List<MeasurementViewModel>();
            foreach (var m in hipsDTO)
            {
                hipsViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Hips";
            return PartialView("_TablePartial", hipsViewModel);
        }

        // GET: Measurements/GetAllThigh
        public async Task<ActionResult> GetAllThigh()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var thighDTO = thighMeasurementService.GetAllSortedByDate(user.Id);
            var thighViewModel = new List<MeasurementViewModel>();
            foreach (var m in thighDTO)
            {
                thighViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Thigh";
            return PartialView("_TablePartial", thighViewModel);
        }

        // GET: Measurements/GetAllCalf
        public async Task<ActionResult> GetAllCalf()
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var calfDTO = calfMeasurementService.GetAllSortedByDate(user.Id);
            var calfViewModel = new List<MeasurementViewModel>();
            foreach (var m in calfDTO)
            {
                calfViewModel.Add(MapMeasurementDTO(m));
            }
            ViewBag.Section = "Calf";
            return PartialView("_TablePartial", calfViewModel);
        }

        // POST: Measurements/CreateWeight
        [HttpPost]
        public async Task<JsonResult> CreateWeight(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new WeightMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = weightMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    weightMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    weightMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateHeight
        [HttpPost]
        public async Task<JsonResult> CreateHeight(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new HeightMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = heightMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    heightMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    heightMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateUpperArm
        [HttpPost]
        public async Task<JsonResult> CreateUpperArm(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new UpperArmMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = upperArmMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    upperArmMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    upperArmMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateForeArm
        [HttpPost]
        public async Task<JsonResult> CreateForeArm(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new ForeArmMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = foreArmMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    foreArmMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    foreArmMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateNeck
        [HttpPost]
        public async Task<JsonResult> CreateNeck(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new NeckMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = neckMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    neckMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    neckMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateChest
        [HttpPost]
        public async Task<JsonResult> CreateChest(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new ChestMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = chestMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    chestMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    chestMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateWaist
        [HttpPost]
        public async Task<JsonResult> CreateWaist(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new WaistMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = waistMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    waistMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    waistMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateHips
        [HttpPost]
        public async Task<JsonResult> CreateHips(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new HipsMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = hipsMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    hipsMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    hipsMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateThigh
        [HttpPost]
        public async Task<JsonResult> CreateThigh(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new ThighMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = thighMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    thighMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    thighMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/CreateCalf
        [HttpPost]
        public async Task<JsonResult> CreateCalf(MeasurementViewModel viewModel)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            if (ModelState.IsValid)
            {
                var measurement = new CalfMeasurement
                {
                    MeasurementDate = viewModel.MeasurementDate,
                    Value = Convert.ToDouble(viewModel.Value)
                };
                var oldMeasurement = calfMeasurementService.Find(m =>
                    m.ApplicationUser.Id == user.Id &&
                    m.MeasurementDate == measurement.MeasurementDate).ToList();
                if (oldMeasurement.Count == 0)
                {
                    measurement.ApplicationUser = user;
                    calfMeasurementService.Create(measurement);
                    return Json("Created successfully");
                }
                else
                {
                    oldMeasurement.First().Value = measurement.Value;
                    calfMeasurementService.Update(oldMeasurement.First());
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

        // POST: Measurements/DeleteWeight/5
        [HttpPost, ActionName("DeleteWeight")]
        public async Task<JsonResult> DeleteWeightConfirmed(int id)
        {
            //var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            //if (user == null)
            //{
            //    return Json(HttpStatusCode.Unauthorized.ToString());
            //}
            //bool res = weightMeasurementService.Delete(id, user.Id);
            //if (res)
            //{
            //    return Json("Deleted successfully");
            //}
            //return Json("Deleted unsuccessfully");

            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = weightMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            weightMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteHeight/5
        [HttpPost, ActionName("DeleteHeight")]
        public async Task<JsonResult> DeleteHeightConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = heightMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            heightMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteUpperArm/5
        [HttpPost, ActionName("DeleteUpperArm")]
        public async Task<JsonResult> DeleteUpperArmConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = upperArmMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            upperArmMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteForeArm/5
        [HttpPost, ActionName("DeleteForeArm")]
        public async Task<JsonResult> DeleteForeArmConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = foreArmMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            foreArmMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteNeck/5
        [HttpPost, ActionName("DeleteNeck")]
        public async Task<JsonResult> DeleteNeckConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = neckMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            neckMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteChest/5
        [HttpPost, ActionName("DeleteChest")]
        public async Task<JsonResult> DeleteChestConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = chestMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            chestMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteWaist/5
        [HttpPost, ActionName("DeleteWaist")]
        public async Task<JsonResult> DeleteWaistConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = waistMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            waistMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteHips/5
        [HttpPost, ActionName("DeleteHips")]
        public async Task<JsonResult> DeleteHipsConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = hipsMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            hipsMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteThigh/5
        [HttpPost, ActionName("DeleteThigh")]
        public async Task<JsonResult> DeleteThighConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = thighMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            thighMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        // POST: Measurements/DeleteCalf/5
        [HttpPost, ActionName("DeleteCalf")]
        public async Task<JsonResult> DeleteCalfConfirmed(int id)
        {
            var user = await userManager.FindByIdAsync(User.Identity.GetUserId());
            if (user == null)
            {
                return Json(HttpStatusCode.Unauthorized.ToString());
            }
            var measurement = calfMeasurementService.Get(id);
            if (measurement == null)
            {
                return Json("Deleted unsuccessfully");
            }
            if (measurement.ApplicationUser.Id != user.Id)
            {
                return Json("Deleted unsuccessfully");
            }
            calfMeasurementService.Delete(id);
            return Json("Deleted successfully");
        }

        private MeasurementViewModel MapMeasurementDTO(MeasurementDTO model)
        {
            var viewModel = new MeasurementViewModel
            {
                Id = model.Id,
                MeasurementDate = model.MeasurementDate,
                Value = model.Value,
                Difference = model.Difference,
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
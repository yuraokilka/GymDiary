using GymDiary.DAL.Entities;
using GymDiary.WebUI.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GymDiary.WebUI.Controllers
{
    [Authorize(Roles = "Trainer, User")]
    [RequireHttps]
    public class ManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ManageController()
        {

        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: /Manage/Index
        public ActionResult Index()
        {
            ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                var model = new ManageViewModel
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    TrainerName = user.TrainerName,
                    Email = user.Email,
                    Sex = user.Sex,
                    BirthDate = user.BirthDate,

                    HasPassword = HasPassword(),
                };
                return View(model);
            }
            else
            {
                return View();
            }
        }

        // POST: /Manage/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ManageViewModel model, HttpPostedFileBase upload)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.TrainerName = model.TrainerName;
                user.Email = model.Email;
                user.Sex = model.Sex;
                user.BirthDate = model.BirthDate;

                if (upload != null && upload.ContentLength > 0)
                {
                    user.ImageMimeType = upload.ContentType;
                    user.ImageData = new byte[upload.ContentLength];
                    upload.InputStream.Read(user.ImageData, 0, upload.ContentLength);
                    //using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    //{
                    //    model.ImageData = reader.ReadBytes(upload.ContentLength);
                    //}
                    //context.SaveChanges();
                }

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                }
            }
            return View(model);
        }

        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            return View(model);
        }

        public async Task<ActionResult> SetDefaultImage()
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                var image = GetDefaultImage();
                user.ImageData = image.ImageData;
                user.ImageMimeType = image.ImageMimeType;
                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                }
            }
            return RedirectToAction("Index", "Manage");
        }

        public FileContentResult GetImage()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null &&
                user.ImageData != null &&
                user.ImageMimeType != null)
            {
                return File(user.ImageData, user.ImageMimeType);
            }
            else
            {
                var image = GetDefaultImage();
                return File(image.ImageData, image.ImageMimeType);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Helpers

        private class UserImage
        {
            public byte[] ImageData { get; set; }
            public string ImageMimeType { get; set; }
        }

        private UserImage GetDefaultImage()
        {
            var image = new UserImage();
            image.ImageData = ImageToByteArray(Properties.Resources.UserPlaceholder);
            image.ImageMimeType = new System.Drawing.ImageFormatConverter().ConvertToString(
                Properties.Resources.UserPlaceholder.RawFormat);
            return image;
        }

        private byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";
        private readonly object FileType;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion
    }
}
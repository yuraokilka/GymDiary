using GymDiary.DAL.EF;
using GymDiary.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GymDiary.WebUI
{
    public class EmailService : IIdentityMessageService
    {
        private const string _emailFromAddress = "no-reply@gmail.com"; // add as default email
        private const string _emailFromName = "GymDiary"; // add as default name
        private const string _smtpAddress = "smtp.gmail.com";
        private const int    _smtpPort = 587;

        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendAsync(message);
        }

        private async Task ConfigSendAsync(IdentityMessage message)
        {
            MailAddress from = new MailAddress(_emailFromAddress, _emailFromName);
            MailAddress to = new MailAddress(message.Destination);
            MailMessage myMail = new MailMessage(@from, to)
            {
                Subject = message.Subject,
                SubjectEncoding = System.Text.Encoding.UTF8,
                Body = message.Body,
                BodyEncoding = System.Text.Encoding.UTF8,
                IsBodyHtml = true
            };

            var mySmtpClient = new SmtpClient(_smtpAddress, _smtpPort)
            {
                Credentials = new NetworkCredential(
                    ConfigurationManager.AppSettings["mailAccount"],
                    ConfigurationManager.AppSettings["mailPassword"]),
                EnableSsl = true
            };

            await mySmtpClient.SendMailAsync(myMail);
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) { }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            manager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = 
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(
            IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }
}

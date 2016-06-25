using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymDiary.WebUI.Startup))]
namespace GymDiary.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

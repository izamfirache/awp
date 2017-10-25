using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LearningPlatform.Startup))]
namespace LearningPlatform
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

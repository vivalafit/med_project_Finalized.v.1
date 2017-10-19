using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(med_project_finalized.Startup))]
namespace med_project_finalized
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

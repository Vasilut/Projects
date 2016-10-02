using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EvaluatorArchitecture.Startup))]
namespace EvaluatorArchitecture
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

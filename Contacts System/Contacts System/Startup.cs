using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Contacts_System.Startup))]
namespace Contacts_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

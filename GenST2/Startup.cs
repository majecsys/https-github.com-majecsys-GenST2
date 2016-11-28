using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GenST2.Startup))]
namespace GenST2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

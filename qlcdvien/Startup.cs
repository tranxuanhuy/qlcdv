using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(qlcdvien.Startup))]
namespace qlcdvien
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

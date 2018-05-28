using System.Web.Http;

namespace Web.Api.App_Start
{
    public class Bootstrapper
    {
        public static void Run()
        {
            //Configure Autofac
            AutofacConfig.Initialize(GlobalConfiguration.Configuration);
        }
    }
}
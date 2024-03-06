namespace Ordrering.API.Extentions;

public static class HostExtentions
{
   public static void AddAppConfigurations(this ConfigureHostBuilder host)
   {
      host.ConfigureAppConfiguration((context,config) =>
      {
         var env = context.HostingEnvironment;
         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true,reloadOnChange:true).AddEnvironmentVariables();

      });
   }
}
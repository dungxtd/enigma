using EnigmaShared.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace EnigmaApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureAppsettings(typeof(Program), new string[] {
                "Enigma.json"
            }).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<StartUp>();
            });
    }
}
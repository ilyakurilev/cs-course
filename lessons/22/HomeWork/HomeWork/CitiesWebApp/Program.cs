using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CitiesWebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder().
                ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>()).
                Build().
                Run();
        }
    }
}

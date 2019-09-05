using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace eroller.web
{
    public class KestrelHosting
    {
        public void Run() {
            var port = 8080;
            var url = $"http://localhost:{port}";

            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseUrls(url)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
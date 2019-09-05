using System;
using Nancy.Hosting.Self;

namespace eroller.web
{
    public class ERollerSelfHosting
    {
        public void Run() {
            var port = 8080;
            var url = $"http://localhost:{port}";

            using (var host = new NancyHost(new Bootstrapper(), new Uri(url))) {
                host.Start();
                Console.WriteLine($"Running on {url}");
                Console.WriteLine("Press <Enter> to terminate...");
                Console.ReadLine();
            }
        }

    }
}
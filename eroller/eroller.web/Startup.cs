using Nancy.Owin;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace eroller.web
{
    public class Startup
    {
        private readonly IConfiguration _config;
        
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            _config = builder.Build();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            ConfigurationBinder.Bind(_config, null);
            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new Bootstrapper()));
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace Grammar.Checker.Portal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => 
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            AddRootDirectory(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string syncfusionLicenseKey = Configuration["Syncfusion:LicenseKey)"];
            SyncfusionLicenseProvider.RegisterLicense(syncfusionLicenseKey);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            MapControllersForEnvironments(app, env);
        }

        private static void AddRootDirectory(IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.RootDirectory = "/Views/Pages";
            });
        }

        private static void MapControllersForEnvironments(
            IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
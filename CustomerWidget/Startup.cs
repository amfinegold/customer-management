using System;
using System.Diagnostics;
using System.Reflection;
using CustomerWidget.Common.Configuration;
using CustomerWidget.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace CustomerWidget.Api
{
    public class Startup
    {
        private readonly Container _container = new Container();

        // Extract file version
        internal static readonly Lazy<string> FileVersion = new Lazy<string>(() => FileVersionInfo
            .GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup the container
            _container.Options.AllowOverridingRegistrations = true;
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            IocInitializer.InitializeContainer(_container, FileVersion.Value);

            // Register configuration
            _container.RegisterInstance(Configuration.GetSection("Database").Get<DatabaseConfig>());


            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

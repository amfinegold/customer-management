using System;
using System.Diagnostics;
using System.Reflection;
using CustomerWidget.Common.Configuration;
using CustomerWidget.Ioc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
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

            // TODO This is where I would put CORS setup if I knew what it should look like.
            // I have had the fortune to deploy APIs exclusively through API gateways
            // that have handled most of the CORS nitty-gritty and accordingly don't have a good sense
            // of what's needed here, and rather than spend the time to research it, 
            // I figured I'd take care of the requirements ask and leave this as a learning opportunity.
            
            //services.AddCors();
            
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Wires simple injector to native .net core IOC
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
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

            // Verify the container integrity
            _container.RegisterMvcControllers(app);
            _container.Verify();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using BackgroundJobs.Data;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using BackgroundJobs.Services;
using BackgroundJobs.Interfaces;
using BackgroundJobs.Models;
using System.Reflection;
using System.Linq;

namespace BackgroundJobs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Assembly assembly = typeof(ModelExecution).Assembly;

            foreach (var type in assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract))
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IExecution<>))
                    {
                        // NOTE: Due to a limitation of Microsoft.DependencyInjection we cannot 
                        // register an open generic interface type without also having an open generic 
                        // implementation type. So, we convert to a closed generic interface 
                        // type to register.
                        var interfaceType = typeof(IExecution<>).MakeGenericType(i.GetGenericArguments());
                        services.AddScoped(interfaceType, type);
                    }
                }
            } // https://stackoverflow.com/questions/48851985/make-net-core-di-auto-resolve-class-by-generic-interface-abstract-class-imple


            services.AddDbContext<ModelFlowContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));
            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddControllers()
                    .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHangfireDashboard();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

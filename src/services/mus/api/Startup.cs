using api.Services;
using application;
using application.common.interfaces;
using FluentValidation.AspNetCore;
using infrastructure;
using infrastructure.identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag.Generation.Processors.Security;
using NSwag;
using persistence;
using System.Linq;
using System.Text;

namespace api
{
    public class Startup
    {
        private IServiceCollection _services;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddInfrastructure(Configuration, Environment);
            services.AddPersistence(Configuration);
            services.AddApplication();
            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                .AddDbContextCheck<MusDbContext>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddOpenApiDocument(configure =>
            {
                configure.Title = "Mus API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });



            this._services = services;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //RegisteredServicesPage(app);
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //  var initialiser = app.ApplicationServices.GetService<DbInitializer>();
            //     await initialiser.InitialiseAsync();
            //    await initialiser.TrySeedAsync();
            //}

            //app.UseCustomExceptionHandler();
            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();

            app.UseOpenApi();

            app.UseSwaggerUi3(settings => {
                settings.Path = "/swagger";
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseIdentityServer();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }

        //private void RegisteredServicesPage(IApplicationBuilder app)
        //{
        //    app.Map("/services", builder => builder.Run(async context =>
        //    {
        //        var sb = new StringBuilder();
        //        sb.Append("<h1>Registered Services</h1>");
        //        sb.Append("<table><thead>");
        //        sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
        //        sb.Append("</thead><tbody>");
        //        foreach (var svc in _services)
        //        {
        //            sb.Append("<tr>");
        //            sb.Append($"<td>{svc.ServiceType.FullName}</td>");
        //            sb.Append($"<td>{svc.Lifetime}</td>");
        //            sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
        //            sb.Append("</tr>");
        //        }
        //        sb.Append("</tbody></table>");
        //        await context.Response.WriteAsync(sb.ToString());
        //    }));
        //}
    }
}

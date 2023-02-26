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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            services.AddInfrastructure();
            services.AddPersistence(Configuration);
            services.AddApplication();
            services.AddHttpContextAccessor();

            services.AddControllers();

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddHealthChecks()
                .AddDbContextCheck<MusDbContext>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //o.Authority = Configuration["JWT:Authority"];
                //o.Audience = Configuration["JWT:Audience"];

                o.Authority = "http://localhost:8080/auth/realms/mus";
                o.Audience = "mus-app";

                o.IncludeErrorDetails = true;

                o.RequireHttpsMetadata = false;
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = StatusCodes.Status418ImATeapot;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(" Authority: " + o.Authority + " Audience: " + o.Audience + " Ex: " + c.Exception.Message + "SYSTEM.DATETIME: " + System.DateTime.UtcNow.ToString());
                    }
                };
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = Configuration["JWT:AuthorityUrl"],
                    ValidIssuer = "http://localhost:8080",
                    ValidateLifetime = true,
                };
            });

            services.AddAuthorization();

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
                app.UseHsts();
            }


            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();

            app.UseOpenApi();

            app.UseSwaggerUi3(settings => {
                settings.Path = "/swagger";
            });

            app.UseCors("AllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using linklives_api_dal;
using linklives_api_dal.Repositories;
using Elasticsearch.Net;

namespace linklives_api
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

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "linklives_api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string> { { "read:links", "Read links from database" }, { "add:link", "Add new link" } },
                            AuthorizationUrl = new Uri("https://" + Configuration["Auth0:Domain"] + "/authorize?audience=" + Configuration["Auth0:Audience"]),
                            TokenUrl = new Uri("https://" + Configuration["Auth0:Domain"] + "/oauth/token")
                        }
                    }
                });
                c.OperationFilter<AuthorizationOperationFilter>();
            });
            #region auth0
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = domain;
                    options.Audience = Configuration["Auth0:Audience"];
                    // If the access token does not have a `sub` claim, `User.Identity.Name` will be `null`. Map it to a different claim by setting the NameClaimType below.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = ClaimTypes.NameIdentifier
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("add:link", policy => policy.Requirements.Add(new HasScopeRequirement("add:link", domain)));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            #endregion

            services.AddDbContext<LinklivesContext>(options =>
           options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));
            var settings = new ConnectionConfiguration(new Uri("https://data-dev.link-lives.dk"))
                .RequestTimeout(TimeSpan.FromMinutes(2));
            services.AddSingleton<ElasticLowLevelClient>(new ElasticLowLevelClient(settings));

            services.AddScoped<ILifeCourseRepository, EFLifeCourseRepository>();
            services.AddScoped<ILinkRepository, EFLinkRepository>();
            services.AddScoped<ILinkRatingRepository, EFLinkRatingRepository>();
            services.AddScoped<IPersonAppearanceRepository, ESPersonAppearanceRepository>();
            services.AddScoped<IRatingOptionRepository, EFRatingOptionRepository>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}

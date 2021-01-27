using System.IO;
using Argo.MyProject.Bl;
using Argo.MyProject.Contracts;
using Argo.MyProject.Middleware;
using Argo.MyProject.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using PostSharp.Patterns.Diagnostics;

#pragma warning disable 1591 // XML Comments

namespace Argo.MyProject
{
    [Log(AttributeExclude = true)]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services">The services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions(); 

            // https://dotnetcoretutorials.com/2019/12/19/using-newtonsoft-json-in-net-core-3-projects/
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            // https://www.c-sharpcorner.com/article/a-better-approach-to-access-httpcontext-outside-a-controller-in-net-core-2-1/
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            // Add your BL classes to the DI engine.
            services.AddScoped<IMyProjectBl, MyProjectBl>();

            #region Configure Swagger

            services.ConfigureSwaggerGen(options =>
            {
                options.CustomSchemaIds(x => x.FullName);
                options.OperationFilter<SwaggerAddHeaderParameters>();
            });

            services.AddSwaggerGen(c =>
            {
                c.MapType<JObject>(() => new OpenApiSchema { Type = "object" });
                // TODO: Change the title
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Argo MyProject", Version = "v1" });
                // Using XML Docs, the Swagger UI page will display them to the consumers of the APi.  This is a good thing.
                // TODO: Change the name of the xml file.  If not using XML comments, delete this part.
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Argo.MyProject.xml");
                if (File.Exists(filePath))
                    c.IncludeXmlComments(filePath);
                // Add other XML file references here.  For example, a DTO project.
            });

            services.AddSwaggerGenNewtonsoftSupport();

            #endregion

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseCookiePolicy(); // Before UseAuthentication or anything else that writes cookies. 

            app.UseMiddleware<ServiceTraceMiddleware>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // TODO: Change the name of the project
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Argo MyProject");
            });

        }

    }
}

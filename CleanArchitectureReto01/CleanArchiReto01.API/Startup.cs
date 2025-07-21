using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CleanArchiReto01.Application.UseCases;
using CleanArchiReto01.Domain.Interfaces;
using CleanArchiReto01.Infrastructure.Persistence;
using Microsoft.OpenApi.Models;

using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CleanArchiReto01.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Adding Swagger for API documentation
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "Reto01 API",
                        Version = "v1",
                        Description = "Reto01 Del Máster Desarrollo de Microservicios en C# y .NET",
                        Contact = new OpenApiContact
                        {
                            Name = "Carlos Clemente",
                            Email = "car.clemente@gmail.com",
                            Url = new Uri("https://github.com/CarlosClemente")
                        }
                    });
                    // Enable XML comments for Swagger
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    options.IncludeXmlComments(xmlPath);
                }
            );

            // Inyecting dependencies
            services.AddSingleton<ITodoTaskRepository, TodoTaskRepository>();
            services.AddTransient<CreateTodoTaskUseCase>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable Swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Reto01 API V1");
                c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
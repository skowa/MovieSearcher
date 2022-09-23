using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Space.MovieSearcher.Application.Configuration;
using Space.MovieSearcher.Infrastructure.Configuration;
using Space.MovieSearcher.Presentation.Api.Extensions;

namespace Space.MovieSearcher.Presentation.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieSearcher", Version = "v1", Description = "API for searching movies" });
            });

            services.AddImdbProvider(Configuration.GetSection("Imdb").Bind);
            services.AddInfrastrucutreServices(Configuration, Configuration.GetSection("SmtpSettings").Bind);
            services.AddHostedServices();
            services.AddApplicationServices();

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<Startup>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionResponsesHander();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieSearcher v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

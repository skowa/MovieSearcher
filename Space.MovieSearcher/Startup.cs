using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Space.MovieSearcher.Application.Extensions;
using Space.MovieSearcher.Infrastructure.Extensions;
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
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ApiAnnotations.xml"));
            });

            services.AddImdbProvider(Configuration.GetSection("Imdb").Bind);
            services.AddInfrastrucutreServices(Configuration, Configuration.GetSection("SmtpSettings").Bind, Configuration.GetValue<string>("Migrations:Assembly"));
            services.AddHostedServices(Configuration.GetSection("EmailOfferJob").Bind);
            services.AddApplicationServices(Configuration.GetSection("EmailOffer").Bind);

            services.AddFluentValidationAutoValidation();

            services.AddValidatorsFromAssemblyContaining<Startup>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<ExceptionHandlerMiddleware> logger)
        {
            app.UseExceptionResponsesHandler(logger);

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

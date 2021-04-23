using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace FirstRespository.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddControllers()
                .AddFluentValidation(options =>
                {
                    options.AutomaticValidationEnabled = true;
                    options.RegisterValidatorsFromAssembly(GetType().Assembly);
                    options.ValidatorOptions.CascadeMode = FluentValidation.CascadeMode.Continue;
                    options.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                });

            serviceCollection.AddAutoMapper(GetType().Assembly);
            serviceCollection.AddOpenApiDocument(options =>
            {
                options.Title = "First Respository";
                options.Version = "1";
            });
        }
        public void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseOpenApi();
            applicationBuilder.UseSwaggerUi3();
            applicationBuilder.UseReDoc();
            applicationBuilder.UseRouting();
            applicationBuilder.UseEndpoints(options =>
            {
                options.MapDefaultControllerRoute();
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System;
using FirstRespository.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace FirstRespository.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<AppDbContext>(options => 
            {
                options.UseInMemoryDatabase("InMemoryDatabase");
            });

            serviceCollection
                .AddIdentity<IdentityUser<Guid>, IdentityRole<Guid>>(options =>
                {
                    options.User.RequireUniqueEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 5;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 1;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = int.MaxValue;
                    options.Lockout.AllowedForNewUsers = true;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            serviceCollection
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var secret = Encoding.UTF8.GetBytes("This is my very very secret key and we should keep it secret...");
                    var symmetricSecurityKey = new SymmetricSecurityKey(secret);

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "http://localhost:5000",
                        ValidateAudience = true,
                        ValidAudience = "http://localhost:5000",
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = symmetricSecurityKey,
                        RequireExpirationTime = true,
                    };
                });


            serviceCollection.AddAuthorization(options =>
            {
                options.AddPolicy("User", policy => policy.RequireRole("User"));
                options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
            });

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

            applicationBuilder.UseAuthentication();
            applicationBuilder.UseAuthorization();

            applicationBuilder.UseEndpoints(options =>
            {
                options.MapDefaultControllerRoute();
            });
        }
    }
}

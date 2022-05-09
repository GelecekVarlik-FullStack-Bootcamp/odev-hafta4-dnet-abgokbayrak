using BMS.Dal.Abstract;
using BMS.Dal.Concrete.EntityFramework.Context;
using BMS.Dal.Concrete.EntityFramework.Repository;
using BMS.Dal.Concrete.EntityFramework.UnitOfWork;
using BMS.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BMS.Bll.ServiceExtensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationContext(this IServiceCollection services)
        {
            services.AddDbContext<BmsContext>();
            services.AddScoped<DbContext, BmsContext>();
        }

        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericManager<,>));
            services.AddScoped<IEmployeeService, EmployeeManager>();
            services.AddScoped<IRequestService, RequestManager>();
            services.AddScoped<ITokenService, TokenManager>();
        }

        public static void AddRepositoryDependency(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidIssuer = configuration["Tokens:Issuer"],
                            ValidAudience = configuration["Tokens:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Tokens:Key"])),
                            RequireSignedTokens = true,
                            RequireExpirationTime = true
                        };
                    });
        }
    }
}

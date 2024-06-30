using System.Text;
using FlexCoach.APIs.Errors;
using FlexCoach.APIs.Helpers;
using FlexCoach.APIs.Middlewares;
using FlexCoach.Core;
using FlexCoach.Core.Services.Contract;
using FlexCoach.Repository;
using FlexCoach.Service.AuthService;
using FlexCoach.Service.CoachService;
using FlexCoach.Service.HashService;
using FlexCoach.Service.ImageUploadService;
using FlexCoach.Service.LoginService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FlexCoach.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddScoped(typeof(IUploadService), typeof(UploadService));

			services.AddScoped(typeof(ICoachService), typeof(CoachService));

			services.AddScoped<ExceptionMiddleware>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped(typeof(IAccountService), typeof(AccountService));

			services.AddAutoMapper(typeof(MappingProfile));

			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (actionContext) =>
				{

					var errors = actionContext.ModelState
												   .Where(P => P.Value.Errors.Count > 0)
												   .SelectMany(P => P.Value.Errors)
												   .Select(E => E.ErrorMessage)
												   .ToList();
					var response = new ApiValidationErrorResponse() { Errors = errors };

					return new BadRequestObjectResult(response);
				};
			});

			return services;
		}
		public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:ValidIssuer"],
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudience"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.Zero
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy("Trainee", policy =>
					policy.RequireRole("Trainee"));
			});

			services.AddScoped(typeof(IAuthService), typeof(AuthService));
			services.AddScoped(typeof(IHashService), typeof(HashService));

			return services;
		}
	}
}

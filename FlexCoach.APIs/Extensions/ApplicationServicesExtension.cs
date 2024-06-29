using FlexCoach.APIs.Errors;
using FlexCoach.APIs.Helpers;
using FlexCoach.APIs.Middlewares;
using FlexCoach.Core;
using FlexCoach.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexCoach.APIs.Extensions
{
	public static class ApplicationServicesExtension
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddScoped<ExceptionMiddleware>();

			services.AddScoped<IUnitOfWork, UnitOfWork>();

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
			return services;
		}
	}
}

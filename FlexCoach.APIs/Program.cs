
using FlexCoach.APIs.Extensions;
using FlexCoach.APIs.Middlewares;
using FlexCoach.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexCoach.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			#region Configure Services

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddApplicationServices();

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddAuthServices(builder.Configuration);

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("MyPolicy", policyOptions =>
				{
					policyOptions.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
				});
			});

			#endregion


			var app = builder.Build();


			app.UseMiddleware<ExceptionMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}

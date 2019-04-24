using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Quorum.Shared.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddModels(this IServiceCollection services,
		                             Assembly                assembly,
		                             string                  classPostfix = "Model")
		{
			var modelTypes = assembly.GetTypesWithPostfix(classPostfix);

			foreach (var modelType in modelTypes)
			{
				services.AddScoped(modelType);
			}
		}

		public static void AddAutoMapper(this IServiceCollection services, Assembly assembly)
		{
			Mapper.Initialize(config => { config.AddProfiles(assembly); });

			var mapper = new Mapper(Mapper.Configuration);

			services.AddSingleton<IMapper>(mapper);

			EntityExtensions.Mapper = mapper;
		}
		
		public static void AddClientCors(this IServiceCollection services, IConfiguration configuration)
		{
			var policy = new CorsPolicyBuilder()
			             .WithOrigins(configuration["Cors:Client:Host"])
			             .AllowAnyHeader()
			             .AllowAnyMethod()
			             .Build();

			services.AddCors(cors => cors.AddPolicy("Client", policy));
		}
	}
}
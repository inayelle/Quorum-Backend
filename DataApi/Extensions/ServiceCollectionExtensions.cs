using System;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quorum.BusinessCore.Models.Challenge;
using Quorum.DataAccess.EfDataProvider.Extensions;
using Quorum.DataApi.Enums;
using Quorum.Shared.Extensions;

namespace Quorum.DataApi.Extensions
{
	internal static class ServiceCollectionExtensions
	{
		public static void AddApiMvc(this IServiceCollection services)
		{
			services.AddMvc().AddJsonOptions(options =>
					options.SerializerSettings
					       .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);
		}

		public static void AddDataProvider(this IServiceCollection services,
		                                   DataProvider            provider,
		                                   string                  connectionString)
		{
			switch (provider)
			{
				case DataProvider.EntityFramework:
				{
					services.AddEfDataAccess(connectionString);
					break;
				}
				default:
				{
					throw new NotImplementedException($"Unimplemented {provider} provider");
				}
			}
		}

		public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			        .AddJwtBearer(options =>
			        {
				        options.RequireHttpsMetadata      = false;
				        options.TokenValidationParameters = configuration.GetValidationParameters();
			        });
		}

		private static TokenValidationParameters GetValidationParameters(this IConfiguration configuration)
		{
			var signKeyBytes = Encoding.UTF8.GetBytes(configuration["Authentication:SignKey"]);
			var signKey      = new SymmetricSecurityKey(signKeyBytes);

			return new TokenValidationParameters
			{
					ValidateIssuer           = true,
					ValidateAudience         = true,
					ValidateLifetime         = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer              = configuration["Authentication:Issuer"],
					ValidAudience            = configuration["Authentication:Audience"],
					IssuerSigningKey         = signKey
			};
		}
	}
}
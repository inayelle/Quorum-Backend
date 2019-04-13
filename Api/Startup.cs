using AspNetCore.RouteAnalyzer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quorum.Api.Extensions;
using Quorum.Shared.Auxiliary;

namespace Quorum.Api
{
	internal sealed class Startup
	{
		public IHostingEnvironment Environment   { get; }
		public IConfiguration      Configuration { get; }

		public Startup(IHostingEnvironment environment)
		{
			Environment   = Require.NotNull(environment, nameof(environment));
			Configuration = Environment.GetConfiguration();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRouteAnalyzer();
			services.AddSingleton(Configuration);
			services.AddAngularCors(Configuration);
			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app)
		{
			app.UseAngularCors();
			app.UseMvc(router =>
			{
				router.MapRouteAnalyzer("/routes");
			});
		}
	}
}
﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Quorum.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
			              .UseStartup<Startup>();
		}
	}
}
using FunctionApp.Functions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctionApp.Startup))]

namespace AzureFunctionApp
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			var configuration = new ConfigurationBuilder()
				.AddEnvironmentVariables()
				.Build();

			builder.Services.Configure<IConfiguration>(configuration);

			builder.Services.AddHttpContextAccessor();
			builder.Services.AddSingleton<MailSenderService>();

		}
	}
}
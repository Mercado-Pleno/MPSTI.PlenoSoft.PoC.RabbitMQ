using FunctionApp.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionApp.Functions.Http
{
	public class HttpFunction
	{
		[FunctionName("HttpFunction")]
		[return: ServiceBus("AzureWebJobsServiceBusQueue", Connection = "AzureWebJobsServiceBus")]
		public async Task<Relatorio> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "Fibonacci")] HttpRequest request, ExecutionContext context, ILogger log)
		{
			log.LogInformation($"{context.FunctionName} - C# HTTP trigger function HttpFunction processed a request and Queue Request in ServiceBus.");

			var quantidade = int.TryParse(request.Query["quantidade"], out var value) ? value : 1;

			return await Task.FromResult(new Relatorio { Quantidade = quantidade });
		}
	}
}
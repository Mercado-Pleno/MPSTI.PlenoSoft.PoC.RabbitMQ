using Common;
using FunctionApp.Domains;
using FunctionApp.Functions.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FunctionApp.Functions.Queue
{
	public class ServiceBusFunction
	{
		public MailSenderService MailSender { get; }
		public ServiceBusFunction(MailSenderService mailSender)
		{
			MailSender = mailSender;
		}


		[FunctionName("ServiceBusFunction")]
		public async Task Run([ServiceBusTrigger("AzureWebJobsServiceBusQueue", Connection = "AzureWebJobsServiceBus")] Relatorio relatorio, ExecutionContext context, ILogger log)
		{
			log.LogInformation($"{context.FunctionName} - C# ServiceBus queue trigger function processed message: {relatorio.Quantidade}");

			var fibonacciService = new FibonacciService();
			var fibonacciSequence = await fibonacciService.GerarAsync(relatorio.Quantidade);
			var fibonacci = string.Join(", ", fibonacciSequence);

			var response = await MailSender.Send(fibonacci, "developer-test@mercadopleno.com.br");

			log.LogInformation($"{context.FunctionName} - C# ServiceBus queue trigger function processed message: {fibonacci}. Send Mail: {response.IsSuccessStatusCode}");
		}
	}
}
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace FunctionApp.Functions.Services
{
	public class MailSenderService
	{
		private readonly string SendGridApiKey;

		public MailSenderService(IConfiguration configuration)
		{
			SendGridApiKey = configuration.GetValue<string>("SendGridApiKey");
		}

		public async Task<Response> Send(string plainTextContent)
		{
			var sendGridMessage = MailHelper.CreateSingleEmail(
				from: new EmailAddress("developer-test@email-temp.com", "Developer Test"),
				to: new EmailAddress("developer-test@email-temp.com", "Developer Test"), // https://generator.email/developer-test@email-temp.com
				subject: "Sending Fibonacci Sequence with SendGrid is Fun",
				plainTextContent,
				htmlContent: $"<strong>{plainTextContent}</strong>"
			);

			var sendGridClient = new SendGridClient(SendGridApiKey);

			return await sendGridClient.SendEmailAsync(sendGridMessage);
		}
	}
}
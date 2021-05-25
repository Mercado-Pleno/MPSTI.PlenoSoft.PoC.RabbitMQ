using Common;
using RabbitMQ.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Publisher
{
	public static class Publisher
	{
		public static void Main(string[] args)
		{
			Execute().Wait();
		}

		public static async Task Execute()
		{
			var factory = new ConnectionFactory() { HostName = Config.RabbitMQAddress };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.QueueDeclare(queue: Config.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

			var restClient = new RestClient(Config.ApiAddress);
			var restRequest = new RestRequest("Order", Method.GET, DataFormat.Json);

			var restResponse = await restClient.ExecuteAsync(restRequest);
			while (restResponse.StatusCode == HttpStatusCode.OK)
			{
				var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(restResponse.Content);
				Console.WriteLine($"{DateTime.Now:HH:mm:ss} {orders.Count()} - {orders.FirstOrDefault()?.OrderId}");

				var tasks = new List<Task>();
				foreach (var order in orders)
				{
					var json = JsonSerializer.Serialize(order);
					var body = Encoding.UTF8.GetBytes(json);
					channel.BasicPublish(exchange: "", routingKey: Config.QueueName, basicProperties: null, body: body);

					var task = MarcarComoProcessado(restClient, order);
					tasks.Add(task);
				}
				Task.WaitAll(tasks.ToArray());

				Thread.Sleep(1000);
				restResponse = await restClient.ExecuteAsync(restRequest);
				while (restResponse.StatusCode != HttpStatusCode.OK)
					Thread.Sleep(1000);
			}

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}

		private static async Task MarcarComoProcessado(RestClient restClient, Order order)
		{
			var postRequest = new RestRequest("Order", Method.PUT, DataFormat.Json);
			postRequest.AddParameter("orderId", order.OrderId, ParameterType.QueryString);

			var postResponse = await restClient.ExecuteAsync(postRequest);
			if (postResponse.StatusCode == HttpStatusCode.OK)
				Console.WriteLine(order.OrderId + " Ok");
		}
	}
}
using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RabbitMQ.Consummer
{
	public static class Subscriber
	{
		public static readonly List<string> orders = new List<string>();
		public static void Main(string[] args)
		{
			var factory = new ConnectionFactory() { HostName = Config.RabbitMQAddress };
			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.QueueDeclare(queue: Config.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += onReceived;

			channel.BasicConsume(queue: Config.QueueName, autoAck: true, consumer: consumer);

			Console.WriteLine(" Press [enter] to exit.");
			Console.ReadLine();
		}

		private static async void onReceived(object model, BasicDeliverEventArgs ea)
		{
			var body = ea.Body.ToArray();
			var json = Encoding.UTF8.GetString(body);
			var order = JsonSerializer.Deserialize<Order>(json);

			if ((order != null) && !orders.Any(id => order.OrderId == id))
			{
				Console.WriteLine(order.OrderId);
				orders.Add(order.OrderId);

				//await MarcarComoProcessado(order);
			}

		}

		private static async Task MarcarComoProcessado(Order order)
		{
			var restClient = new RestClient(Config.ApiAddress);
			var restRequest = new RestRequest("Order", Method.PUT, DataFormat.Json);
			restRequest.AddParameter("orderId", order.OrderId, ParameterType.QueryString);

			var restResponse = await restClient.ExecuteAsync(restRequest);
			if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
				Console.WriteLine(order.OrderId + " Ok");
		}
	}
}

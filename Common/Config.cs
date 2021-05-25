using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	public static class Config
	{
		public static string ApiAddress => "https://localhost:44391/";
		public static string RabbitMQAddress => "localhost";
		public static string QueueName => "ecommerce.pedido.recebido";
	}
}

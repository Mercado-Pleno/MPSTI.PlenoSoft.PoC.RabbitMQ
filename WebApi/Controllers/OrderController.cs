using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly ILogger<OrderController> _logger;
		private static readonly List<Order> _orders = new List<Order>();
		public OrderController(ILogger<OrderController> logger) { _logger = logger; }

		[HttpGet]
		public async Task<IEnumerable<Order>> Get()
		{
			return await Task.FromResult(_orders.Where(o => o.Active).Take(200));
		}

		[HttpPut]
		public async Task<IActionResult> Put(string orderId)
		{
			var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
			await order?.Process();
			return Ok();
		}

		[HttpPost]
		public async Task<IActionResult> Post(int quantidade)
		{
			return await Task.Factory.StartNew(() =>
			{
			   var orders = Order.Generate(quantidade);
			   _orders.AddRange(orders);
			   return Ok();
			});
		}
	}
}
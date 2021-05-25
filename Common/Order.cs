using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
	public class Order
	{
		[JsonPropertyName("orderId")]
		public string OrderId { get; set; }

		[JsonPropertyName("creationDate")]
		public DateTime CreationDate { get; set; }

		[JsonPropertyName("clientName")]
		public string ClientName { get; set; }

		[JsonPropertyName("items")]
		public object Items { get; set; }

		[JsonPropertyName("totalValue")]
		public double TotalValue { get; set; }

		[JsonPropertyName("paymentNames")]
		public string PaymentNames { get; set; }

		[JsonPropertyName("status")]
		public string Status { get; set; }

		[JsonPropertyName("statusDescription")]
		public string StatusDescription { get; set; }

		[JsonPropertyName("marketPlaceOrderId")]
		public object MarketPlaceOrderId { get; set; }

		[JsonPropertyName("sequence")]
		public long Sequence { get; set; }

		[JsonPropertyName("salesChannel")]
		public string SalesChannel { get; set; }

		[JsonPropertyName("affiliateId")]
		public string AffiliateId { get; set; }

		[JsonPropertyName("origin")]
		public string Origin { get; set; }

		[JsonPropertyName("workflowInErrorState")]
		public bool WorkflowInErrorState { get; set; }

		[JsonPropertyName("workflowInRetry")]
		public bool WorkflowInRetry { get; set; }

		[JsonPropertyName("lastMessageUnread")]
		public string LastMessageUnread { get; set; }

		[JsonPropertyName("ShippingEstimatedDate")]
		public DateTime ShippingEstimatedDate { get; set; }

		[JsonPropertyName("ShippingEstimatedDateMax")]
		public DateTime ShippingEstimatedDateMax { get; set; }

		[JsonPropertyName("ShippingEstimatedDateMin")]
		public DateTime ShippingEstimatedDateMin { get; set; }

		[JsonPropertyName("orderIsComplete")]
		public bool OrderIsComplete { get; set; }

		[JsonPropertyName("listId")]
		public object ListId { get; set; }

		[JsonPropertyName("listType")]
		public object ListType { get; set; }

		[JsonPropertyName("authorizedDate")]
		public DateTime AuthorizedDate { get; set; }

		[JsonPropertyName("callCenterOperatorName")]
		public object CallCenterOperatorName { get; set; }

		[JsonPropertyName("totalItems")]
		public int TotalItems { get; set; }

		[JsonPropertyName("currencyCode")]
		public string CurrencyCode { get; set; }

		[JsonPropertyName("hostname")]
		public string Hostname { get; set; }

		[JsonPropertyName("invoiceOutput")]
		public object InvoiceOutput { get; set; }

		[JsonPropertyName("invoiceInput")]
		public object InvoiceInput { get; set; }

		public bool Active { get; set; }
		private static int _id = 0;
		private static readonly Random _random = new();
		private static readonly DateTime _date = DateTime.UtcNow.AddDays(-30);

		public async Task Process()
		{
			await Task.Factory.StartNew(() =>
			{
				var timeOut = TimeSpan.FromMilliseconds(_random.Next(2000, 6000));
				Thread.Sleep(timeOut);
				Active = false;
			});
		}

		public static Order Generate()
		{
			return new Order
			{
				OrderId = $"v{++_id:00000000}epcc-01",
				CreationDate = DateTime.Now.AddMilliseconds(_id),
				ClientName = "CLIENTE ANONIMIZADO",
				Items = null,
				TotalValue = 0.01 + (_random.NextDouble() * 100000),
				PaymentNames = "Mastercard",
				Status = "ready-for-handling",
				StatusDescription = "Pronto para o manuseio",
				MarketPlaceOrderId = null,
				Sequence = _id + _random.Next(0, 2345678),
				SalesChannel = _random.Next(0, 9).ToString(),
				AffiliateId = "",
				Origin = "Marketplace",
				WorkflowInErrorState = false,
				WorkflowInRetry = false,
				LastMessageUnread = $"pagamento do seu pedido foi aprovado {_random.Next(1, 56244782)}",
				ShippingEstimatedDate = _date.AddSeconds(_id),
				ShippingEstimatedDateMax = _date.AddDays(_id),
				ShippingEstimatedDateMin = _date.AddHours(_id),
				OrderIsComplete = true,
				ListId = null,
				ListType = null,
				AuthorizedDate = _date.AddMinutes(_id),
				CallCenterOperatorName = null,
				TotalItems = _random.Next(1, 15),
				CurrencyCode = "BRL",
				Hostname = "epocacosmeticos",
				InvoiceOutput = null,
				InvoiceInput = null,
				Active = true
			};
		}

		public static Order[] Generate(int quantidade)
		{
			return Enumerable.Range(0, quantidade).Select(i => Generate()).ToArray();
		}
	}
}
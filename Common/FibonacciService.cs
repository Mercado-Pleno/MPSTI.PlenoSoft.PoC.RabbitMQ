using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
	public class FibonacciService
	{
		private static readonly Fibonacci _fibonacci = new Fibonacci();

		public IEnumerable<long> Gerar(int count) => GerarAsync(count).Result;

		public async Task<IEnumerable<long>> GerarAsync(int count)
		{
			return await _fibonacci.Get(count);
		}

		public int Count()
		{
			return _fibonacci.Count();
		}

		public bool IsRunning => _fibonacci.IsRunning;
	}

	public class Fibonacci
	{
		private readonly object _access = new object();
		private readonly List<long> _sequence;
		private long N1 { get; set; }
		private long N2 { get; set; }
		public bool IsRunning { get; private set; }

		public Fibonacci()
		{
			N1 = 0;
			N2 = 1;
			_sequence = new List<long> { N1, N2 };
		}

		public int Count() => _sequence.Count;

		public async Task<IEnumerable<long>> Get(int count)
		{
			await Generate(count);
			return _sequence.Take(count);
		}

		private async Task Generate(int count)
		{
			if (_sequence.Count <= count)
			{
				lock (_access)
				{
					IsRunning = true;
					while (_sequence.Count <= count)
					{
						var next = Next();
						_sequence.Add(next);
						Thread.Sleep(500);
						//Thread.Sleep(Convert.ToInt32(Math.Max(Math.Min(next, Int32.MaxValue), 0)));
					}
					IsRunning = false;
				}
				await Task.Delay(500);
			}
		}

		private long Next()
		{
			var n3 = N1 + N2;
			N1 = N2;
			return N2 = n3;
		}
	}
}
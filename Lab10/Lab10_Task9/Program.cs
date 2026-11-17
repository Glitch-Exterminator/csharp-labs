namespace Lab10_Task9
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<decimal> prices = [14.99m, 23.49m, 2049.99m, 1200, 155];

			Func<decimal, decimal> noDiscount = price => price;
			Func<decimal, decimal> discount10 = price => price * 0.9m;
			Func<decimal, decimal> discount20 = price => price > 1000 ? (price * 0.8m) : price;

			Console.WriteLine($"{Total(prices, noDiscount)}");
			Console.WriteLine($"{Total(prices, discount10)}");
			Console.WriteLine($"{Total(prices, discount20)}");
		}

		static decimal Total(IEnumerable<decimal> prices, Func<decimal, decimal> discount)
		{
			decimal total = 0;
			foreach (decimal price in prices)
				total += discount(price);
			return total;
		}
	}
}
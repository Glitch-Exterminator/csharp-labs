namespace Lab11_Task3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello, World!");
		}

		static public decimal PriceByStatus(int amount, string status)
		{
			decimal month = 2000m;
			switch (status)
			{
				case "silver":
					month *= 0.9m;
					break;
				case "gold":
					month *= 0.75m;
					break;
			}

			return month * amount;
		}
	}
}
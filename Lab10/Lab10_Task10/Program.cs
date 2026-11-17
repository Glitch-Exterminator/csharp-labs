namespace Lab10_Task10
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(string.Join(", ", Fibonacci().Take(10)));
			Console.WriteLine(string.Join(", ", Fibonacci().Where(n => n % 2 == 0).Take(5)));
			Console.WriteLine(string.Join(", ", Fibonacci().TakeWhile(n => n < 100)));
		}

		static IEnumerable<long> Fibonacci()
		{
			long a = 0, b = 1;
			while (true)
			{
				yield return a;
				long temp = a; a = b;
				b += temp;
			}
		}
	}
}
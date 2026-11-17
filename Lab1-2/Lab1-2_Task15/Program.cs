namespace Lab1_2_Task15
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Write("Fibonacci: "); int n = int.Parse(Console.ReadLine());
			Console.WriteLine(fib(n));
		}

		static Dictionary<int, long> fibonacci = [];
		static long fib(int n)
		{
			if (n <= 2) return 1;
			if (fibonacci.ContainsKey(n)) return fibonacci[n];
			return fibonacci[n] = fib(n - 1) + fib(n - 2);
		}
	}
}
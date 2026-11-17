namespace Lab10_Task6
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Operation sum = (a, b) => a + b;
			Operation substract = (a, b) => a - b;
			Operation multiply = (a, b) => a * b;
			Operation divide = (a, b) =>
			{
				if (b == 0)
				{
					// throw new InvalidOperationException("Делить на 0 нельзя!");
					Console.Write("Делить на 0 нельзя! ");
					return 0;
				}
				return a / b;
			};

			Console.WriteLine(Calculate(3, 7, sum));
			Console.WriteLine(Calculate(5, 7, substract));
			Console.WriteLine(Calculate(4, 4, multiply));
			Console.WriteLine(Calculate(10, 0, divide));
			Console.WriteLine(Calculate(0, 10, divide));
		}
		public static int Calculate(int a, int b, Operation op) => op(a, b);
	}

	internal delegate int Operation(int a, int b);
}
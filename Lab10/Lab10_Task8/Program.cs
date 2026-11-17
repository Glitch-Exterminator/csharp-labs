namespace Lab10_Task8
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Action<int> count = (i) => Console.WriteLine(i + 1);
			Repeat(11, count);
		}

		public static void Repeat(int times, Action<int> doSomething)
		{
			for (int i = 0; i < times; i++) doSomething(i);
		}
	}
}
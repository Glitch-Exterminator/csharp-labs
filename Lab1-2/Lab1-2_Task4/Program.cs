namespace Lab1_2_Task4
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Write("n = "); int n = int.Parse(Console.ReadLine());
			List<int> list = new();
			for (int i = 0; i < n; i++) list.Add(1 + i);

			Console.Write("k = "); int k = int.Parse(Console.ReadLine());

			for (int i = 0; i < k; i++)
			{
				Console.WriteLine();
				Console.Write("Value = "); int number = int.Parse(Console.ReadLine());
				Console.Write("Index = "); int index = int.Parse(Console.ReadLine());

				if (index == list.Count) list.Add(number);
				else list.Insert(index, number);
			}

			Console.WriteLine("\n" + string.Join(", ", list));
		}
	}
}
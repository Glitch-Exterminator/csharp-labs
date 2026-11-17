namespace Lab1_2_Task5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, string> dictionary = new();

			Console.Write("n = "); int n = int.Parse(Console.ReadLine());
			for (int i = 0; i < n; i++)
			{
				Console.WriteLine();
				string word_1 = Console.ReadLine(), word_2 = Console.ReadLine();
				dictionary[word_1] = word_2;
				dictionary[word_2] = word_1;
			}

			Console.WriteLine("\n");
			Console.WriteLine(dictionary[Console.ReadLine()]);
		}
	}
}
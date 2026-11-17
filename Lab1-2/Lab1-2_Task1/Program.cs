namespace Lab1_2_Task1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			uint counter = 0;
			string word;

			while ((word = Console.ReadLine()) != null)
			{
				if (word != "") counter++;
			}

			Console.WriteLine(counter);
		}
	}
}
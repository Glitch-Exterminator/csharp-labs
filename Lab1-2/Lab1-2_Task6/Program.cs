namespace Lab1_2_Task6
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, int> dictionary = new();
			Console.Write("Amount of states: ");  int statesCount = int.Parse(Console.ReadLine());

			Console.WriteLine();
			for (int i = 0; i < statesCount; i++)
			{
				string[] nameVotes = Console.ReadLine().Split(" ");
				string name = nameVotes[0];
				int votes = int.Parse(nameVotes[1]);

				if (!dictionary.ContainsKey(name)) dictionary.Add(name, votes);
				else dictionary[name] += votes;
			}

			Console.WriteLine();
			foreach (KeyValuePair<string, int> pair in dictionary)
				Console.WriteLine($"{pair.Key} {pair.Value}");
		}
	}
}
namespace Lab9_Task4
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<int> integers = [1, 4, 2, -1, 0, 77, 47];
			Console.WriteLine(integers.FindMax());

			List<string> alphabet = ["A", "d", "D", "e", "1"];
			Console.WriteLine(alphabet.FindMax());

			List<Player> players = [];
			players.Add(new Player("Игрок 1", 2));
			players.Add(new Player("Игрок 2", 3));
			players.Add(new Player("Игрок 3", 3));
			players.Add(new Player("Игрок 4", 1));
			Console.WriteLine(players.FindMax().Name);
		}
	}

	internal record Player(string Name, int Score) : IComparable<Player>
	{
		public int CompareTo(Player other)
		{
			if (other is null) return 1;
			return Score.CompareTo(other.Score);
		}
	}
}
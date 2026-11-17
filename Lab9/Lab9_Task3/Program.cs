namespace Lab9_Task3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Reward> rewardsPull = [];
			rewardsPull.Add(new Reward("30 coins", 30));
			rewardsPull.Add(new Reward("100 coins", 100));
			rewardsPull.Add(new Reward("1000 coins", 1000));

			for (int i = 0; i < 10; i++)
				Console.WriteLine($"You've won {rewardsPull.GetRandomElement().Name}.");
		}
	}

	internal record Reward(string Name, int Gold);
}
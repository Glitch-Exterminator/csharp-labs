namespace Lab11_Task1
{
	internal class Potion
	{
		public string Title { get; }
		public int HealingPower { get; }

		public Potion(string title, int healingPower)
		{
			Title = title;
			HealingPower = healingPower;
		}

		public override string ToString() => $"Title: {Title}\nHealing power: {HealingPower}";

	}
}
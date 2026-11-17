namespace Lab15_Task3
{
	internal class Monster : IPrototype<Monster>
	{
		public string Name { get; set; }
		public int Health { get; set; }
		public List<string> Loot { get; set; } = [];
		public List<string> Abilities { get; set; } = [];

		public Monster(string name, int health)
		{
			Name = name;
			Health = health;
		}

		public Monster Clone()
		{
			// Глубокая копия: клон получает СОБСТВЕННЫЙ список лута
			Monster clone = (Monster)MemberwiseClone();
			clone.Loot = new(Loot);
			clone.Abilities = new(Abilities);
			return clone;
		}

		public override string ToString()
		{
			string info = $"{Name,-18} HP: {Health,3} | Лут: {string.Join(", ", Loot)}";
			if (Abilities.Count > 0)
				info += $" | Умения: {string.Join(", ", Abilities)}";
			return info;
		}
	}
}
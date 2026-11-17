namespace Lab11_Task1
{
	internal class Inventory
	{
		private readonly List<Potion> potions = [];

		public void AddPotion(Potion p) => potions.Add(p);

		public override string ToString()
		{
			string result = potions[0].ToString();
			for (int i = 1; i < potions.Count; i++)
				result += "\n\n" + potions[i];

			return result;
		}
	}
}
namespace Lab15_Task3
{
	internal class Bestiary
	{
		private readonly Dictionary<string, Monster> prototypes = new Dictionary<string, Monster>();

		public void Register(string key, Monster prototype)
		{
			prototypes[key] = prototype;
		}

		public Monster Spawn(string key)
		{
			return prototypes[key].Clone();
		}
	}
}
namespace Lab11_Task2
{
	internal class Pet
	{
		public string Name { get; set; }
		public int Hunger { get; private set; } = 50;
		public int Happiness { get; private set; } = 50;

		public Pet(string name)
		{
			Name = name;
		}

		public void Feed()
		{
			Hunger -= 10;
			Happiness += 5;
		}

		public void Play()
		{
			Happiness += 10;
			Hunger += 5;
		}

		public override string ToString() => $"Name: {Name}\nHunger level: {Hunger}\nHappiness: {Happiness}";
	}
}
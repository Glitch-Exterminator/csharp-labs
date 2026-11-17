namespace Lab3_Task3
{
	internal class SpaceShip
	{
		public string name;
		public int crew = 5;
		public byte fuelLevel = 50, shieldStrength = 75;

		public SpaceShip(string name) => this.name = name;

		public SpaceShip(string name, int crew) : this(name)
		{
			this.crew = crew;
		}

		public SpaceShip(string name, int crew, byte fuelLevel) : this(name, crew)
		{
			this.fuelLevel = fuelLevel;
		}

		public SpaceShip(string name, int crew, byte fuelLevel, byte shieldStrength) : this(name, crew, fuelLevel)
		{
			this.shieldStrength = shieldStrength;
		}

		public SpaceShip(SpaceShip spaceShip)
		{
			name = spaceShip.name + " Clone";
			crew = spaceShip.crew;
			fuelLevel = spaceShip.fuelLevel;
			shieldStrength = spaceShip.shieldStrength;
		}

		public void DisplayInfo()
		{
			Console.WriteLine($"Name: {name}");
			Console.WriteLine($"Crew: {crew}");
			Console.WriteLine($"Fuel: {fuelLevel}%");
			Console.WriteLine($"Shield: {shieldStrength}%");
		}

		public void TakeDamage(byte damage)
		{
			shieldStrength = (byte) Math.Max(0, shieldStrength - damage);
		}

		~SpaceShip() => Console.WriteLine($"Spaceship \"{name}\" disappeared from radar.");
	}
}
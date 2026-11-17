namespace Lab8_Task5
{
	internal class Player : IAttack, IBuild
	{
		public string Name { get; }
		public int HP { get; private set; }
		public int Attack {  get; }

		public Player(string name, int hp = 20, int attack = 1)
		{
			Name = name;
			HP = hp;
			Attack = attack;
		}

		void IAttack.Action()
			=> Console.WriteLine($"Игрок атакует, нанося {Attack} урона.");

		void IBuild.Action()
			=> Console.WriteLine("Игрок строит.");
	}
}
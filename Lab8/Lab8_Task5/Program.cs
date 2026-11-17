namespace Lab8_Task5
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Player player = new Player("Стив");

			IAttack fighter = player;
			fighter.Action();

			IBuild builder = player;
			builder.Action();
		}
	}
}
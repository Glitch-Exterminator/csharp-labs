namespace Lab3_Task3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SpaceShip alpha = new SpaceShip("Alpha");
			SpaceShip beta = new SpaceShip("Beta", 7);
			SpaceShip release = new SpaceShip("Release", 4, 95);
			SpaceShip update = new("Update v0.9", 3, 100, 100);
			SpaceShip update2 = new(update);

			update2.DisplayInfo();
		}
	}
}
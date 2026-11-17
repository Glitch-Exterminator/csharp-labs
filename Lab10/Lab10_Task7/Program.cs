namespace Lab10_Task7
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Action greet = () => Console.WriteLine("Привет!");
			for (int i = 0; i < 3; i++) greet();

			Console.WriteLine();
			Func<int> roll = () => Random.Shared.Next(1, 7);
			for (int i = 0; i < 5; i++) Console.WriteLine(roll()); ;
		}
	}
}
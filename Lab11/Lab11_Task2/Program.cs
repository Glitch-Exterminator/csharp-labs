namespace Lab11_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Pet garfield = new("Garfield");
			Console.WriteLine(garfield);

			garfield.Play();
			garfield.Feed();
			Console.WriteLine("\n" + garfield);
		}
	}
}
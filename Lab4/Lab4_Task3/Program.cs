namespace Lab4_Task3
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DateTime dayX = DateTime.MaxValue;

			Console.WriteLine($"Existense of the Universe left: {(dayX - DateTime.UtcNow).TotalDays / 365} years");
		}
	}
}
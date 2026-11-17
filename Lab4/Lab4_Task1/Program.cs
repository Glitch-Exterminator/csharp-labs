namespace Lab4_Task1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			TimeOnly startWork = new TimeOnly(8, 0);
			TimeOnly endWork = new TimeOnly(18, 0);

			TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
			Console.WriteLine((now >= startWork && now <= endWork) ? "Wake up, Erjan." : "Sleepy time");
		}
	}
}
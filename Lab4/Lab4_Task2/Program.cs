namespace Lab4_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DateTime utc = DateTime.UtcNow;
			Console.WriteLine(utc);

			TimeZoneInfo europeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
			DateTime europeTime = TimeZoneInfo.ConvertTimeFromUtc(utc, europeZone);
			Console.WriteLine(europeTime);

			TimeZoneInfo tokyoZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
			DateTime tokyoTime = TimeZoneInfo.ConvertTimeFromUtc(utc, tokyoZone);
			Console.WriteLine(tokyoTime);
		}
	}
}
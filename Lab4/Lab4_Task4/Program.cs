namespace Lab4_Task4
{
	internal class Program
	{
		static void Main(string[] args)
		{
			DateTimeOffset powerOff = DateTimeOffset.Parse("2026-01-13T08:00:00+02:00");
			DateTimeOffset powerOn = DateTimeOffset.Parse("2026-01-13T23:50:00+02:00");

			TimeSpan outageSpan = powerOn - powerOff;
			DateOnly outageDate = DateOnly.FromDateTime(powerOff.DateTime);
			TimeOnly outageTime = TimeOnly.FromDateTime(powerOff.DateTime);

			Console.WriteLine($"{outageDate.ToString("yyyy.MM.dd")} {outageTime} - outage lasted for {outageSpan.Hours}H {outageSpan.Minutes}m");
		}
	}
}
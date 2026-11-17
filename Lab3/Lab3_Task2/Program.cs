namespace Lab3_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			PersonManager manager = new();

			PersonManager.AddPerson(new Person(12345678, "Alpha Tester", 25, "City 1"));
			PersonManager.AddPerson(new Person(23456789, "Beta Tester", 20, "City 2"));
			PersonManager.PrintPeople();

			PersonManager.UpdateAddress(23456789, "City 1"); Console.WriteLine();
			Console.WriteLine(PersonManager.FindByName("Beta Tester"));
		}
	}
}
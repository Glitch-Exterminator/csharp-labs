namespace Lab1_2_Task9
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Passport passport = new Passport("Name", "Surname");
			passport.PrintName();
			/*
			Console.WriteLine();

			for (int index = 0; index < length; index++)
			{
			}

			foreach (var item in collection)
			{
			}
			*/
		}
	}

	class Passport
	{
		private string firstName, lastName;

		public Passport(string firstName, string lastName)
		{
			this.firstName = firstName;
			this.lastName = lastName;
		}

		public Passport() {}	

		public void PrintName() => Console.WriteLine(firstName);
	}
}
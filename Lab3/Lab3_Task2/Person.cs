namespace Lab3_Task2
{
	internal class Person
	{
		public int PersonID;
		public string Name;
		public int Age;
		public string Address;

		public Person(int personID, string name, int age, string address)
		{
			PersonID = personID;
			Name = name;
			Age = age;
			Address = address;
		}

		public void SetAddress(string address)
		{
			Address = address;
			Console.WriteLine($"{PersonID}: address updated.");
		}

		public override string ToString() => $"ID: {PersonID}\nName: {Name}\nAge: {Age}\nAddress: {Address}";
	}
}
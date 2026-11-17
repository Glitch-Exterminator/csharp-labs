using System.Net;

namespace Lab5_6_Task1
{
	internal class Person
	{
		public int PersonID
		{
			get => field;
			private set
			{
				if (value <= 0)
					throw new ArgumentException("Invalid ID!");
				field = value;
			}
		}

		public string Name
		{
			get => field;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException("Name canot be empty");
				field = value;
			}
		}

		public DateOnly Birthday { get; }

		public int Age
		{
			get
			{
				DateOnly today = DateOnly.FromDateTime(DateTime.Today);

				int age = today.Year - Birthday.Year;
				if (today < Birthday.AddYears(age)) age--;
				return age;
			}
		}

		public string Address
		{
			get => field;
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException("Name cannot be empty");
				field = value;
			}
		}

		public Person(int personID, string name, DateOnly birthday, string address)
		{
			PersonID = personID;
			Name = name;
			Birthday = birthday;
			Address = address;
		}

		public override string ToString() => $"ID: {PersonID}\nName: {Name}\nAge: {Age}\nAddress: {Address}";
	}
}
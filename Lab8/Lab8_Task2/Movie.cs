namespace Lab8_Task2
{
	internal class Movie : IComparable<Movie>
	{
		public string Name
		{
			get => field;
			set
			{
				if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("Empty name.");
				field = value;
			}
		}
		public double Rating
		{
			get => Math.Round(field, 2);
			set => field = Math.Clamp(value, 0, 10);
		}
		public TimeSpan Duration { get; }
		public int Year { get; }

		public Movie(string name, double rating, TimeSpan duration, int year)
		{
			Name = name;
			Rating = rating;
			Duration = duration;
			Year = year;
		}

		public int CompareTo(Movie other)
		{
			if (other == null) return 1;
			return this.Name.CompareTo(other.Name);
		}
	}
}
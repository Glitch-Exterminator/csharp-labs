namespace Lab8_Task1
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

		public int CompareTo(Movie movie)
		{
			if (movie == null) return 1;
			return Rating.CompareTo(movie.Rating);
		}
	}
}
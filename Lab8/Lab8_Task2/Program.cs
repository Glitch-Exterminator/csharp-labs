namespace Lab8_Task2
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Movie> movies = [];
			movies.Add(new Movie("Веном", 6.95, new TimeSpan(1, 52, 0), 2018));
			movies.Add(new Movie("Веном 2", 6.22, new TimeSpan(1, 37, 0), 2021));
			movies.Add(new Movie("Веном 3: Последний танец", 6.60, new TimeSpan(0, 109, 0), 2024));
			movies.Add(new Movie("Б", 6.22, new TimeSpan(1, 0, 0), 2024));



			movies.Sort(new MovieRatingComparer());
			foreach (Movie movie in movies) Console.WriteLine($"{movie.Rating} | \"{movie.Name}\"");

			Console.WriteLine();
			movies.Sort(new MovieDurationComparer());
			foreach (Movie movie in movies) Console.WriteLine($"{movie.Duration} | \"{movie.Name}\"");

			Console.WriteLine();
			movies.Sort(new MovieNewFirstSorter());
			foreach (Movie movie in movies) Console.WriteLine($"{movie.Year} | {movie.Name}");
		}
	}
}
namespace Lab8_Task1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Movie> movies = [];

			movies.Add(new Movie("Веном", 6.95, new TimeSpan(1, 52, 0), 2018));
			movies.Add(new Movie("Веном 2", 6.22, new TimeSpan(1, 37, 0), 2021));
			movies.Add(new Movie("Веном 3: Последний танец", 6.60, new TimeSpan(0, 109, 0), 2024));

			movies.Sort(); movies.Reverse();

			foreach (Movie movie in movies) Console.WriteLine($"\"{movie.Name}: {movie.Rating}");
		}
	}
}
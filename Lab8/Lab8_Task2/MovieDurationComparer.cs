namespace Lab8_Task2
{
	internal class MovieDurationComparer : IComparer<Movie>
	{
		public int Compare(Movie x, Movie y)
		{
			if (x == null && y == null) return 0;
			if (x is null) return -1;
			if (y is null) return 1;

			int result = x.Duration.CompareTo(y.Duration);
			if (result != 0) return result;

			return x.CompareTo(y);
		}
	}
}
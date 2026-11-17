namespace Lab8_Task2
{
	internal class MovieRatingComparer : IComparer<Movie>
	{
		public int Compare(Movie x, Movie y)
		{
			if (x == null && y == null) return 0;
			if (x is null) return -1;
			if (y is null) return 1;

			int result = y.Rating.CompareTo(x.Rating);
			if (result != 0) return result;

			return x.CompareTo(y);
		}
	}
}
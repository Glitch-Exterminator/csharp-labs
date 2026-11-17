namespace Lab8_Task4
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<INumericTransformation> transformations = [new By1Incrementer(), new By2Multiplier(), new ToPowerOf2Raiser()];

			double x = 3;
			Console.WriteLine(x);
			Console.WriteLine(Transform(x, transformations));
		}

		static double Transform(double x, List<INumericTransformation> transformations)
		{
			foreach (INumericTransformation transformation in transformations)
				x = transformation.Transform(x);
			return x;
		}
	}
}
namespace Lab8_Task4
{
	internal class ToPowerOf2Raiser : INumericTransformation
	{
		public double Transform(double value) => Math.Pow(value, 2);
	}
}
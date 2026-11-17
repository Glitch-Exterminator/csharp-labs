namespace Lab9_Task1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			CustomStack<int> list = new CustomStack<int>();

			list.Push(3);
			list.Push(4);
			Console.WriteLine(list.Peek());

			Console.WriteLine(list.Pop());
			Console.WriteLine(list.Peek());
		}
	}
}
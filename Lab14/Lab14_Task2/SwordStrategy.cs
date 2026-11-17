namespace Lab14_Task2
{
	internal class SwordStrategy : IWeaponStrategy
	{
		public void Attack()
		{
			Console.WriteLine("Герой бьет мечом в ближнем бою: ВЖУХ!");
		}
	}
}
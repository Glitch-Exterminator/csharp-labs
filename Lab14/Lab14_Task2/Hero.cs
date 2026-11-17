namespace Lab14_Task2
{
	internal class Hero
	{
		private IWeaponStrategy currentWeapon;

		public Hero(IWeaponStrategy startingWeapon)
		{
			this.currentWeapon = startingWeapon;
		}

		public void SetWeapon(IWeaponStrategy newWeapon)
		{
			this.currentWeapon = newWeapon;
			Console.WriteLine("\n[Система] Оружие изменено.");
		}

		public void PerformAttack()
		{
			currentWeapon.Attack(); // Герой просто делегирует работу оружию
		}
	}
}
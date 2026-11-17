namespace Lab3_Task1
{
	internal class SoftwareLicense
	{
		private static int AvailableLicenses = 5;
		private string OwnerName, LicenseKey;
		private bool IsActive;

		public SoftwareLicense(string OwnerName)
		{
			this.OwnerName = OwnerName;

			if (AvailableLicenses > 0)
			{
				LicenseKey = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
				IsActive = true; AvailableLicenses--;
			}
			else LicenseKey = "Not activated.";
		}

		public static void PrintAvailableLicenses() => Console.WriteLine(AvailableLicenses);
		public override string ToString() => $"Owner: {OwnerName}\nKey: {LicenseKey}"; // Не пашет
	}
}
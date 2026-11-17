namespace Lab.Student;

/// <summary>
/// ФАЙЛ 2. Устройство, которое светит с регулировкой яркости.
/// Третий этаж иерархии: ActuatorBase -> LightingBase.
///
/// Что сделать:
///  - реализовать метод установки яркости (value в диапазоне 0..MaxBrightness) и обновить состояние вкл/выкл;
///  - реализовать функционал вкл/выкл текущего устройства;
///  - реализовать Describe().
/// </summary>
public abstract class LightingBase : ActuatorBase
{
	/// <summary>Текущая яркость, 0..MaxBrightness.</summary>
	public int Brightness { get; protected set; } = 0;

	protected LightingBase(string id, string name, string room, double ratedPowerW) : base(id, name, room, ratedPowerW)
	{
	}

	// НОВОЕ абстрактное свойство: его обязан реализовать каждый конечный класс.
	// Объявление уже готово, менять его не нужно.
	public abstract int MaxBrightness { get; }

	public void SetBrightness(int value)
	{
		Brightness = Math.Clamp(value, 0, MaxBrightness);
		IsOn = (Brightness != 0);
	}
	public override void TurnOn() => SetBrightness(MaxBrightness);
	public override void TurnOff() => SetBrightness(0);

	public override string Describe() => $"{base.Describe()} Текущая яркость: {(Brightness/MaxBrightness)*100}%. Паспортная мощность: {RatedPowerW}";
}
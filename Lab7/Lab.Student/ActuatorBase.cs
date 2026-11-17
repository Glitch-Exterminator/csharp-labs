using Lab.Core;

namespace Lab.Student;

/// <summary>
/// ФАЙЛ 1. Устройство, которое можно включить и выключить.
/// Второй этаж иерархии: DeviceBase -> ActuatorBase.
///
/// Что сделать:
///  - реализовать необходимые для корректной работы свойства и методы;
///  - проинициализировать паспортную мощность (отрицательная -> ArgumentException);
///  - добавить автоматическое отключение прибора, когда наступает ночь;
///  - переопределить Equals и GetHashCode — сравнение по Id БЕЗ учёта регистра.
/// </summary>
public abstract class ActuatorBase : DeviceBase
{
	/// <summary>Паспортная мощность, Вт.</summary>
	public double RatedPowerW { get; }

	/// <summary>Включено ли устройство прямо сейчас.</summary>
	public bool IsOn { get; protected set; }

	protected ActuatorBase(string id, string name, string room, double ratedPowerW) : base(id, name, room)
	{
		if (ratedPowerW < 0) throw new ArgumentException("Negative rated power");
		RatedPowerW = ratedPowerW;
	}

	public override string Kind => "Актуатор";

	public virtual void TurnOn() => IsOn = true;
	public virtual void TurnOff() => IsOn = false;
	public override void OnNightMode() => TurnOff();

	public override string Describe() => $"[{Id}] {Name} ({Room}): {(IsOn ? "вкл" : "выкл")}. Паспортная мощность: {RatedPowerW}";

	public override bool Equals(object obj)
		=> obj is ActuatorBase actuatorBase && string.Equals(this.Id, actuatorBase.Id, StringComparison.OrdinalIgnoreCase);

	public override int GetHashCode() => Id.ToLowerInvariant().GetHashCode();
}
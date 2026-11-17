namespace Lab.Core;

/// <summary>
/// ОБРАЗЕЦ. Прочитай этот класс первым — здесь на простом
/// устройстве показано, как реализуется контракт DeviceBase целиком.
///
/// Счётчик электроэнергии. Его нельзя включить или выключить: это НЕ актуатор.
/// Ночной режим он игнорирует — OnNightMode здесь не переопределён,
/// поэтому сработает пустая реализация из DeviceBase.
/// 
/// Что сделать:
///  - реализовать сравнение счётчиков по Id (если Id совпадает, то считать, что объекты одинаковые);
/// </summary>
public class PowerMeter(string id, string name, string room) : DeviceBase(id, name, room)
{
	/// <summary>Накопленное потребление, кВт·ч.</summary>
	public double TotalKWh { get; private set; }

	public override string Kind => "Счётчик";

	public override double CurrentLoadW => 0.4;

	public override void Tick(int minute)
	{
		TotalKWh += 0.0004;
	}

	public override object[] Snapshot() =>
	[
		Id,
		Name,
		Room,
		Kind,
		true,                             // bool упаковывается в object
		Math.Round(CurrentLoadW, 2),      // double упаковывается в object
		$"{TotalKWh:F3} кВт·ч"
	];

	public override void Apply(object[] args)
	{
		if (args == null || args.Length == 0) return;

		string cmd = (string)args[0];     // string — ссылочный тип: приведение, не распаковка

		if (cmd == "reset")
			TotalKWh = 0;
	}

	public override string ToString()
		=> $"Счётчик {Name} ({Room}): {TotalKWh:F3} кВт·ч";

	public override bool Equals(object obj)
		=> obj is PowerMeter powerMeter && string.Equals(this.Id, powerMeter.Id, StringComparison.OrdinalIgnoreCase);

	public override int GetHashCode() => Id.ToLowerInvariant().GetHashCode();
}
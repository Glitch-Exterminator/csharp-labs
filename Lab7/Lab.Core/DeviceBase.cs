namespace Lab.Core;

/// <summary>
/// Общий контракт любого устройства умного дома.
/// НЕ РЕДАКТИРОВАТЬ! Этот класс задаёт правила, по которым работает вся система.
/// </summary>
public abstract class DeviceBase
{
	/// <summary>Уникальный идентификатор, например "hall.lamp".</summary>
	public string Id { get; }

	/// <summary>Отображаемое имя, например "Лампа прихожей".</summary>
	public string Name { get; }

	/// <summary>Комната: "Прихожая", "Гостиная", "Кухня", "Спальня".</summary>
	public string Room { get; }

	protected DeviceBase(string id, string name, string room)
	{
		if (string.IsNullOrWhiteSpace(id))
			throw new ArgumentException("Идентификатор не может быть пустым", nameof(id));

		Id = id;
		Name = name;
		Room = room;
	}

	/// <summary>Категория устройства: "Актуатор", "Счётчик", "Датчик".</summary>
	public abstract string Kind { get; }

	/// <summary>Текущее потребление в ваттах прямо сейчас.</summary>
	public abstract double CurrentLoadW { get; }

	/// <summary>Один шаг симуляции. Вызывается оболочкой каждую "минуту".</summary>
	public abstract void Tick(int minute);

	/// <summary>
	/// Строка для таблицы состояний. Ровно 7 элементов:
	/// Id, Name, Room, Kind, IsOn (bool), CurrentLoadW (double), состояние (string).
	/// </summary>
	public abstract object[] Snapshot();

	/// <summary>
	/// Команда устройству. args[0] — название команды (string),
	/// args[1] — параметр, если он нужен.
	/// </summary>
	public abstract void Apply(object[] args);

	/// <summary>Описание для лога. Наследники ДОПОЛНЯЮТ его.</summary>
	public virtual string Describe() => $"[{Id}] {Name} ({Room})";

	/// <summary>Реакция на ночной режим. По умолчанию — ничего не делать.</summary>
	public virtual void OnNightMode() { }

	public abstract override string ToString();
	public abstract override bool Equals(object obj);
	public abstract override int GetHashCode();
}
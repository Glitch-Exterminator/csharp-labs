namespace Lab9_Task5
{
	internal class Storage<V> where V : IEntity
	{
		private readonly Dictionary<int, V> _entities = [];
		public IReadOnlyCollection<V> Values => _entities.Values;

		public V this[int key]
		{
			get
			{
				if (_entities.ContainsKey(key))
					return _entities[key];
				throw new InvalidOperationException("Not in a dictionary.");
			}
			private set => _entities[key] = value;
		}

		public void Add(V value)
		{
			if (_entities.ContainsKey(value.Id))
				throw new InvalidOperationException("Дубликат.");
			_entities[value.Id] = value;
		}
	}
}
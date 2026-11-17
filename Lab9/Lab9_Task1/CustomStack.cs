namespace Lab9_Task1
{
	internal class CustomStack<T>
	{
		private List<T> _stack = [];
		public int Count => _stack.Count;
		public bool IsEmpty => _stack.Count == 0;

		public void Push(T item) => _stack.Add(item);
		public T Pop()
		{
			if (IsEmpty)
				throw new InvalidOperationException("Stack is empty.");

			int lastIndex = Count - 1;
			T temp = _stack[lastIndex];
			_stack.RemoveAt(lastIndex);
			return temp;
		}

		public T Peek()
		{
			if (IsEmpty)
				throw new InvalidOperationException("Stack is empty.");

			 return _stack[Count - 1]; // Patter "Clone"
		}
	}
}
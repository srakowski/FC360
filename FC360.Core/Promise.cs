namespace FC360.Core
{
	using System;

	public class Promise<T>
	{
		public Promise(Action<Action<T>> promise)
		{
			promise(Resolve);
		}

		public T Result { get; private set; }

		public bool IsComplete { get; private set; }

		private void Resolve(T result)
		{
			Result = result;
			IsComplete = true;
		}
	}
}

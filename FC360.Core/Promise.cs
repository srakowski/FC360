namespace FC360.Core
{
	using System;

	public abstract class Promise
	{
		public abstract bool IsComplete { get; }

		public Promise<T> Of<T>()
		{
			return (Promise<T>)this;
		}
	}

	public class Promise<T> : Promise
	{
		private bool _isComplete;

		public Promise(Action<Action<T>> promise)
		{
			promise(Resolve);
		}

		public T Result { get; private set; }

		public override bool IsComplete => _isComplete;

		private void Resolve(T result)
		{
			Result = result;
			_isComplete = true;
		}
	}
}

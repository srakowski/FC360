namespace FC360.Core
{
	using System;

	public abstract class Program
	{
		public virtual void Init() { }

		public abstract void Update(double deltaInMS);

		public abstract void Draw();
	}
}

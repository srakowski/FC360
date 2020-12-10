namespace FC360.Core
{
	using FC360.Core.Drivers;

	public abstract class Program
	{
		public Program(SysDriver api)
		{
			Sys = api;
		}

		protected SysDriver Sys { get; }

		public virtual void Init() { }

		public abstract void Update(double deltaInMS);

		public abstract void Draw();
	}
}

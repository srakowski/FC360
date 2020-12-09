namespace FC360.Core
{
	public abstract class Program
	{
		public Program(SysApi api)
		{
			Api = api;
		}

		protected SysApi Api { get; }

		public virtual void Init() { }

		public abstract void Update(double deltaInMS);

		public abstract void Draw();
	}
}

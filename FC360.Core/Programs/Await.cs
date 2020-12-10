namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;

	public class Await : Program
	{
		private Promise _promise;

		public Await(SysDriver api, Promise promise) : base(api)
		{
			_promise = promise;
		}

		public override void Update(double deltaInMS)
		{
			if (_promise.IsComplete)
			{
				Sys.ExitProgram(_promise);
			}
		}

		public override void Draw()
		{
			Sys.Console.Clear();
			Sys.Console.Output(0, 0, "LOADING...");
		}
	}
}

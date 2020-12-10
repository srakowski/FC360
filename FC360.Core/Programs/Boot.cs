namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;

	public class Boot : Program
	{
		private Promise<string[]> _gameFiles;

		public Boot(SysDriver api) : base(api) { }

		public override void Init()
		{
			_gameFiles = Sys.FS.GetFiles();
		}

		public override void Update(double deltaInMS)
		{
			if (!_gameFiles.IsComplete)
				return;

			Sys.ExitProgram();
			Sys.RunProgram(new RootMenu(Sys, _gameFiles.Result));
		}

		public override void Draw()
		{
			Sys.Console.Clear();
			Sys.Console.Output(0, 0, "LOADING...");
		}
	}
}

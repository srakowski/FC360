namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;

	class GameLoader : Program
	{
		private readonly string _gameFileName;
		private Promise<byte[]> _gameData;

		public GameLoader(SysDriver sys, string gameFileName) : base(sys)
		{
			_gameFileName = gameFileName;
		}

		public override void Init()
		{
			_gameData = Sys.FS.ReadFile(_gameFileName);
		}

		public override void Update(double deltaInMS)
		{
			if (!_gameData.IsComplete)
				return;

			Sys.LoadGame(_gameFileName, _gameData.Result);
			Sys.ExitProgram();
			Sys.RunProgram(new GameMenu(Sys));
		}

		public override void Draw()
		{
			Sys.Console.Clear();
			Sys.Console.Output(0, 0, "LOADING...");
		}
	}
}

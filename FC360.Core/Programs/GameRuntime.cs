namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;

	class GameRuntime : Program
	{
		private Game _game;

		public GameRuntime(SysDriver api) : base(api) { }

		public override void Init()
		{
			_game = Sys.Game.Execute();
			_game.Init();
		}

		public override void Update(double deltaInMS)
		{
			if (Sys.Input.ButtonWasPressed(Button.Escape))
			{
				Sys.ExitProgram();
				return;
			}

			_game.Update(deltaInMS);
		}

		public override void Draw()
		{
			_game.Draw();
		}
	}
}

namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;

	enum InitialGameMode : byte
	{
		Menu = 0,
		Run,
		Edit
	}

	class GameMenu : Program
	{
		private readonly string _gameFileName;
		private InitialGameMode _mode;
		private Menu _menu;

		public GameMenu(
			SysDriver sys,
			string gameFileName,
			InitialGameMode mode = InitialGameMode.Menu)
			: base(sys)
		{
			_gameFileName = gameFileName;
			_mode = mode;
		}

		public override void Init()
		{
			var gameData = Sys.FS.ReadFile(_gameFileName);
			
			Sys.Game.Load(_gameFileName, gameData);

			_menu = new Menu(
				_gameFileName,
				new Tab("MENU",
					new MenuOption("RUN"),
					new MenuOption("EDIT"),
					new MenuOption("EXIT")
					)
				);

			if (_mode == InitialGameMode.Edit)
			{
				Sys.RunProgram(new EditGame(Sys));
				_mode = InitialGameMode.Menu;
			}
		}

		public override void Update(double deltaInMS)
		{
			var selection = Sys.Menu.Update(_menu);
			switch (selection.MenuOptionIdx)
			{
				case 0: // RUN
					Sys.RunProgram(new GameRuntime(Sys));
					break;

				case 1: // EDIT
					Sys.RunProgram(new EditGame(Sys));
					break;

				case 2: // EXIT
					Sys.ExitProgram();
					break;
			}
		}

		public override void Draw()
		{
			Sys.Menu.Draw(_menu);
		}
	}
}

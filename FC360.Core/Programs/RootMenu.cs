namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;
	using System.Linq;

	class RootMenu : Program
	{
		private string[] _gameFiles;
		private Menu _menu;

		public RootMenu(SysDriver api) : base(api) { }

		public override void Init()
		{
			RefreshMenu();
		}

		public override void Resume(object returnParam)
		{
			RefreshMenu();
		}

		public override void Update(double deltaInMS)
		{
			var selection = Sys.Menu.Update(_menu);
			switch(selection.TabIdx)
			{
				case 0: // GAMES
					var gameFileName = _gameFiles[selection.MenuOptionIdx];
					LoadGameMenu(gameFileName);
					break;

				case 1: // NEW
					CreateGame();
					break;

				case 2: // SYS
					Sys.ExitProgram();
					break;
			}
		}

		public override void Draw()
		{
			Sys.Menu.Draw(_menu);
		}

		private void RefreshMenu()
		{
			_gameFiles = Sys.FS.GetFiles();
			_menu = CreateMenu(_gameFiles);
		}

		private void LoadGameMenu(string gameFileName)
		{
			Sys.RunProgram(new GameMenu(Sys, gameFileName));
		}

		private void CreateGame()
		{
			Sys.RunProgram(new CreateGame(Sys));
		}

		private static Menu CreateMenu(string[] gameFiles)
		{
			var gameMenuOptions = gameFiles
				.Select(g => new MenuOption(g))
				.ToArray();

			return new Menu(
				"FC-360 v0.0",
				new Tab("GAMES", gameMenuOptions),
				new Tab("NEW",
					new MenuOption("CREATE GAME")
					),
				new Tab("SYS",
					new MenuOption("SHUTDOWN")
					)
				);
		}
	}
}

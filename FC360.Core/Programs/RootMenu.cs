namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;
	using System.Linq;

	class RootMenu : Program
	{
		private string[] _gameFiles;
		private Menu _menu;

		public RootMenu(SysDriver api, string[] gameFiles) : base(api)
		{
			_gameFiles = gameFiles;
		}

		public override void Init()
		{
			_menu = CreateMenu(_gameFiles);
		}

		public override void Update(double deltaInMS)
		{
			var selection = Sys.Menu.Update(_menu);
			switch(selection.TabIdx)
			{
				case 0: // GAMES
					var gameFileName = _gameFiles[selection.MenuOptionIdx];
					LoadGame(gameFileName);
					break;

				case 1: // NEW
					CreateGame();
					break;

				case 2: // SYS
					break;
			}
		}

		public override void Draw()
		{
			Sys.Menu.Draw(_menu);
		}

		private void LoadGame(string gameFileName)
		{
			Sys.RunProgram(new GameLoader(Sys, gameFileName));
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

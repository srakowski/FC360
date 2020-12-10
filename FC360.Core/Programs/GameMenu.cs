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
		private readonly InitialGameMode _mode;
		private Menu _menu;

		public GameMenu(SysDriver sys, InitialGameMode mode = InitialGameMode.Menu) : base(sys)
		{
			_mode = mode;
		}

		public override void Init()
		{
			_menu = new Menu(
			Sys.ActiveGameName,
			new Tab("MENU",
				new MenuOption("RUN"),
				new MenuOption("EDIT"),
				new MenuOption("EXIT")
				)
			);

			if (_mode == InitialGameMode.Edit)
			{
				Sys.RunProgram(new EditGame(Sys));
				return;
			}
		}

		public override void Update(double deltaInMS)
		{
			var selection = Sys.Menu.Update(_menu);
			switch (selection.TabIdx)
			{
				case 0: // RUN
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

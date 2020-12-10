using System;

namespace FC360.Core.Programs
{
	class RootMenu : Program
	{
		private Menu _menu;

		public RootMenu(SysApi api) : base(api)
		{
		}

		public override void Init()
		{
			_menu = new Menu(
				"FC-360 v0.0",
				new Tab("GAMES",
					new MenuOption("NONE"),
					new MenuOption("NOTHING"),
					new MenuOption("TEST")
					),
				new Tab("NEW",
					new MenuOption("CREATE GAME")
					),
				new Tab("SYS",
					new MenuOption("SHUTDOWN")
					)
				);
		}

		public override void Update(double deltaInMS)
		{
			var selection = Api.Menu.Update(_menu);
			switch(selection)
			{
				case MenuSelection s when !s.HasValue:
					break;

				case MenuSelection s when s.TabIdx == 1 && s.MenuOptionIdx == 0:
					CreateProgram();
					break;
			};
		}
		public override void Draw()
		{
			Api.Menu.Draw(_menu);
		}

		private void CreateProgram()
		{
			Api.Run(new CreateGame(Api));
		}
	}
}

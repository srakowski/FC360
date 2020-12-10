namespace FC360.Core.Programs
{
	class GameMenu : Program
	{
		private Menu _menu;

		public GameMenu(SysApi api) : base(api) { }

		public override void Init()
		{
			_menu = new Menu(
				Api.ActiveGameName,
				new Tab("MENU",
					new MenuOption("RUN"),
					new MenuOption("EDIT"),
					new MenuOption("EXIT")
					)
				);
		}

		public override void Update(double deltaInMS)
		{
		}

		public override void Draw()
		{
		}
	}
}

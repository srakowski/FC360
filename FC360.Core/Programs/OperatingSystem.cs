namespace FC360.Core.Programs
{
	class OperatingSystem : Program
	{
		private Menu _menu;

		public OperatingSystem(SysApi api) : base(api)
		{
		}

		public override void Init()
		{
			_menu = new Menu(
				"FC-360 v0.0",
				new Tab("PROGS",
					new MenuOption("NONE")
					),
				new Tab("NEW",
					new MenuOption("CREATE PROGRAM")
					),
				new Tab("SYS",
					new MenuOption("SHUTDOWN")
					)
				);
		}

		public override void Update(double deltaInMS)
		{
		}

		public override void Draw()
		{
			Api.Menu.Draw(_menu);
		}
	}
}

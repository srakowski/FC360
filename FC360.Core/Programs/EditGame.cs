namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;

	class EditGame : Program
	{
		public EditGame(SysDriver sys) : base(sys)
		{
		}

		public override void Init()
		{
			Sys.OpenGameEditor();
		}

		public override void Update(double deltaInMS)
		{
			if (Sys.Input.ButtonWasPressed(Button.Escape))
			{
				Sys.ExitProgram();
			}
		}

		public override void Draw()
		{
			Sys.Console.Clear();
			Sys.Console.Output(0, 0, $"EDIT {Sys.ActiveGameName}");
			Sys.Console.InvertRange(0, 0, Sys.Console.BufferWidth, 1);
			Sys.Console.Output(2, 6, "Edit at:");
			Sys.Console.Output(2, 7, "http://localhost:8080/api/fc360");
			Sys.Console.Output(0, Sys.Console.BufferHeight - 1, "[ESC] Return");
		}
	}
}

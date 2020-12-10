namespace FC360.Core.Programs
{
	class EditGame : Program
	{
		public EditGame(SysApi api) : base(api)
		{
		}

		public override void Update(double deltaInMS)
		{
		}

		public override void Draw()
		{
			Api.Text.Clear();
			Api.Text.Output(0, 0, $"EDIT {Api.ActiveGameName}");
			Api.Text.InvertRange(0, 0, Api.Text.BufferWidth, 1);
			Api.Text.Output(2, 6, "Edit at:");
			Api.Text.Output(2, 7, "http://localhost:8080");
			Api.Text.Output(0, Api.Text.BufferHeight - 1, "[ESC] Return");
		}
	}
}

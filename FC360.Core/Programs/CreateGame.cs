namespace FC360.Core.Programs
{
	using FC360.Core.Drivers;
	using System.Text;

	class CreateGame : Program
	{
		private StringBuilder _nameBuffer = new StringBuilder();

		public CreateGame(SysDriver api) : base(api)
		{
		}

		public override void Update(double deltaInMS)
		{
			foreach (var c in Sys.Input.GetTextInput())
			{
				switch (c)
				{
					case '\r':
						if (_nameBuffer.Length > 0)
						{
							CreateGameWithName(_nameBuffer.ToString());
							_nameBuffer.Clear();
						}
						break;

					case '\b':
						if (_nameBuffer.Length > 0)
						{
							_nameBuffer.Remove(_nameBuffer.Length - 1, 1);
						}
						break;

					default:
						_nameBuffer.Append(c);
						break;
				}
			}
		}

		private void CreateGameWithName(string gameName)
		{
			Sys.LoadGame(gameName);
			Sys.ExitProgram();
			Sys.RunProgram(new GameMenu(Sys, InitialGameMode.Edit));
		}

		public override void Draw()
		{
			Sys.Console.Clear();
			Sys.Console.Output(0, 0, "CREATE GAME");
			Sys.Console.InvertRange(0, 0, Sys.Console.BufferWidth, 1);
			var val = $"NAME={_nameBuffer}";
			Sys.Console.Output(0, 1, val);
			Sys.Console.InvertRange(val.Length, 1, 1, 1);
		}
	}
}

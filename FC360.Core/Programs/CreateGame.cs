using System.Diagnostics;
using System.Text;

namespace FC360.Core.Programs
{
	class CreateGame : Program
	{
		private StringBuilder _nameBuffer = new StringBuilder();

		public CreateGame(SysApi api) : base(api)
		{
		}

		public override void Update(double deltaInMS)
		{
			foreach (var c in Api.Input.GetTextInput())
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
			Api.LoadGame(gameName);
			Api.Run(new EditGame(Api));
		}

		public override void Draw()
		{
			Api.Text.Clear();
			Api.Text.Output(0, 0, "CREATE GAME");
			Api.Text.InvertRange(0, 0, Api.Text.BufferWidth, 1);
			var val = $"NAME={_nameBuffer}";
			Api.Text.Output(0, 1, val);
			Api.Text.InvertRange(val.Length, 1, 1, 1);
		}
	}
}

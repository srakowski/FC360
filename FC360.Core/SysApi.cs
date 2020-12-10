using FC360.Core.Programs;
using System;

namespace FC360.Core
{
	public class SysApi
	{
		private FantasyConsole _fc;

		public SysApi(FantasyConsole fc)
		{
			_fc = fc;
			Input = new InputApi(_fc.Mem.InputBuffer);
			Text = new TextApi(_fc.Mem.TextBuffer);
			Menu = new MenuApi(Input, Text);
		}

		public InputApi Input { get; }

		public TextApi Text { get; }

		public MenuApi Menu { get; }

		public string ActiveGameName => _fc.Mem.ActiveGame;

		public void Run(Program prog)
		{
			_fc.PushProgram(prog);
		}

		public void LoadGame(string name)
		{
			_fc.Mem.ActiveGame = name;
			_fc.Mem.SpriteBuffer.Clear();
			_fc.Mem.CodeBuffer.Clear();
		}
	}
}

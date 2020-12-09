namespace FC360.Core
{
	public class SysApi
	{
		private FantasyConsole _fc;

		public SysApi(FantasyConsole fc)
		{
			_fc = fc;
			Text = new TextApi(_fc.Mem.TextBuffer);
			Menu = new MenuApi(Text);
		}

		public TextApi Text { get; }

		public MenuApi Menu { get; }
	}
}

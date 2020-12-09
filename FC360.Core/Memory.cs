namespace FC360.Core
{
	public class Memory
	{
		public Memory(int width, int height)
		{
			DisplayMode = DisplayMode.Text;
			TextBuffer = new TextBuffer(width, height);
			SysFont = new SysFont();
			DisplayBuffer = new DisplayBuffer(width, height);
			Pallete = new Pallete();
		}

		public DisplayMode DisplayMode { get; }

		public TextBuffer TextBuffer { get; }

		public SysFont SysFont { get; }

		public byte TextFGPalleteIdx { get; } = 15;

		public byte TextBGPalleteIdx { get; } = 0;

		public DisplayBuffer DisplayBuffer { get; }

		public Pallete Pallete { get; }
	}
}

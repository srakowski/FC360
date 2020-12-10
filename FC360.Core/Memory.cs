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

			InputBuffer = new InputBuffer();

			SpriteBuffer = new SpriteBuffer();
			CodeBuffer = new CodeBuffer();

			FileSystemBuffer = new FileSystemBuffer();
		}

		// GRAPHICS

		public DisplayMode DisplayMode { get; set; }

		public TextBuffer TextBuffer { get; }

		public SysFont SysFont { get; }

		public byte TextFGPalleteIdx { get; } = 15;

		public byte TextBGPalleteIdx { get; } = 0;

		public DisplayBuffer DisplayBuffer { get; }

		public Pallete Pallete { get; }

		// INPUT

		public InputBuffer InputBuffer { get; }

		// GAME

		// TODO: convert to "interrupt" code
		public bool EditMode { get; set; }

		public string ActiveGame { get; set; }

		public SpriteBuffer SpriteBuffer { get; }

		public CodeBuffer CodeBuffer { get; }

		// FILE SYSTEM

		public FileSystemBuffer FileSystemBuffer { get; }
	}
}

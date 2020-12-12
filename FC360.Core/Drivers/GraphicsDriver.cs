namespace FC360.Core.Drivers
{
	public class GraphicsDriver : Driver
	{
		private Memory _mem;
		private SpriteBuffer _spriteBuffer;
		private DisplayBuffer _displayBuffer;

		public GraphicsDriver(Memory mem)
		{
			_mem = mem;
			_spriteBuffer = mem.SpriteBuffer;
			_displayBuffer = mem.DisplayBuffer;
		}

		public void Clear()
		{
			_displayBuffer.Clear();
		}

		public void GraphicsMode()
		{
			_mem.DisplayMode = DisplayMode.Graphics;
		}

		public void TextMode()
		{
			_mem.DisplayMode = DisplayMode.Text;
		}

		public void DrawSprite(byte i, int x, int y)
		{
			var spriteData = _spriteBuffer[i];
			for (var sy = 0; sy < Sprite.Height; sy++)
			{
				for (var sx = 0; sx < Sprite.Width; sx++)
				{
					_displayBuffer[x + sx, y + sy] = spriteData[sx, sy];
				}
			}
		}
	}
}

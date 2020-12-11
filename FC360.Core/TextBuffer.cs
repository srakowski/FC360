namespace FC360.Core
{
	using System;

	[Flags]
	public enum CharCellFlag : byte
	{
		None,
		Invert,
	}

	public struct CharCell
	{
		public CharCell(char value, CharCellFlag flags)
		{
			Value = value;
			Flags = flags;
		}

		public char Value;
		public CharCellFlag Flags;

		public static implicit operator CharCell(char c) => new CharCell(c, CharCellFlag.None);
	}

	public class TextBuffer
	{
		private CharCell[,] _data;

		public TextBuffer(int displayBufferWidth, int displayBufferHeight)
		{
			_data = new CharCell[
				displayBufferWidth / SysFont.CharWidth,
				displayBufferHeight / SysFont.CharHeight
			];
		}

		public CharCell this[int x, int y]
		{
			get => _data[x, y];
			set
			{
				if (x < 0 || x >= Width ||
					y < 0 || y >= Height)
				{
					return;
				}

				_data[x, y] = value;
			}
		}

		public int Width => _data.GetLength(0);

		public int Height => _data.GetLength(1);
	}
}
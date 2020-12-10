namespace FC360.Core
{
	public class TextApi
	{
		private TextBuffer _textBuffer;

		public TextApi(TextBuffer textBuffer)
		{
			_textBuffer = textBuffer;
		}

		public int BufferWidth => _textBuffer.Width;

		public int BufferHeight => _textBuffer.Height;

		public void Clear()
		{
			for (var y = 0; y < _textBuffer.Height; y++)
			{
				for (var x = 0; x < _textBuffer.Width; x++)
				{
					_textBuffer[x, y] = new CharCell();
				}
			}
		}

		public void Output(int x, int y, string text, bool invert = false)
		{
			for (var i = 0; i < text.Length; i++)
			{
				_textBuffer[x + i, y] = new CharCell(
					text[i], 
					invert ? CharCellFlag.Invert : CharCellFlag.None);
			}
		}

		public void InvertRange(int fromX, int fromY, int columnCount, int rowCount)
		{
			for (var y = fromY; y < fromY + rowCount; y++)
			{
				for (var x = fromX; x < fromX + columnCount; x++)
				{
					var cell = _textBuffer[x, y];
					if (cell.Flags.HasFlag(CharCellFlag.Invert))
					{
						cell.Flags &= ~CharCellFlag.Invert;
					}
					else
					{
						cell.Flags |= CharCellFlag.Invert;
					}
					_textBuffer[x, y] = cell;
				}
			}
		}
	}
}

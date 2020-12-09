using System;

namespace FC360.Core
{
	public class FantasyConsole
	{
		private const int DisplayWidth = 192;
		private const int DisplayHeight = 128;

		private Program _program;

		public FantasyConsole()
		{
			_program = new Programs.OperatingSystem();
			Mem = new Memory(DisplayWidth, DisplayHeight);
		}

		public Memory Mem { get; }

		public void PowerOn()
		{
			_program.Init();
		}

		public void Tick(double deltaInMS)
		{
			_program.Update(deltaInMS);
			_program.Draw();
			if (Mem.DisplayMode == DisplayMode.Text)
			{
				CopyTextToDisplayBuffer();
			}
		}

		private void CopyTextToDisplayBuffer()
		{
			for (var y = 0; y < Mem.TextBuffer.Height; y++)
			{
				for (var x = 0; x < Mem.TextBuffer.Width; x++)
				{
					RenderTextAt(x, y);
				}
			}
		}

		private void RenderTextAt(int cellX, int cellY)
		{
			var charCell = Mem.TextBuffer[cellX, cellY];
			var charBitmap = Mem.SysFont[charCell.Value];
			for (var y = 0; y < SysFont.CharHeight; y++)
			{
				for (var x = 0; x < SysFont.CharWidth; x++)
				{
					var bitIsToggledOn = charBitmap[x, y];
					var bitIsInverted = charCell.Flags.HasFlag(CharCellFlag.Invert);
					bitIsToggledOn = bitIsInverted ? !bitIsToggledOn : bitIsToggledOn;

					var pixelX = (cellX * SysFont.CharWidth) + x;
					var pixelY = (cellY * SysFont.CharHeight) + y;
					Mem.DisplayBuffer[pixelX, pixelY] = bitIsToggledOn
						? Mem.TextFGPalleteIdx
						: Mem.TextBGPalleteIdx;
				}
			}
		}
	}
}

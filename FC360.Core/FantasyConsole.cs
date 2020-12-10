namespace FC360.Core
{
	using FC360.Core.Drivers;
	using System.Collections.Generic;

	public class FantasyConsole
	{
		private const int DisplayWidth = 192;
		private const int DisplayHeight = 128;

		private SysDriver _sys;
		private Stack<Program> _programStack;

		public FantasyConsole()
		{
			Mem = new Memory(DisplayWidth, DisplayHeight);
			_sys = new SysDriver(this);
			_programStack = new Stack<Program>();
		}

		public Memory Mem { get; }

		public void PowerOn()
		{
			PushProgram(new Programs.Boot(_sys));
		}

		internal void PushProgram(Program prog)
		{
			_programStack.Push(prog);
			prog.Init();
		}

		internal void PopProgram()
		{
			_programStack.Pop();
		}

		public void Tick(double deltaInMS)
		{
			_sys.Update(deltaInMS);

			// TODO: maybe not Peek() every time, store current prog?
			_programStack.Peek().Update(deltaInMS);
			_programStack.Peek().Draw();

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

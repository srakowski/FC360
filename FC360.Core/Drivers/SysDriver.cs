﻿namespace FC360.Core.Drivers
{
	using System.Collections.Generic;

	public class SysDriver : Driver
	{
		private FantasyConsole _fc;
		private List<Driver> _drivers;

		public SysDriver(FantasyConsole fc)
		{
			_fc = fc;
			_drivers = new List<Driver>();
			_drivers.Add(FS = new FileSystemDriver());
			_drivers.Add(Input = new InputDriver(_fc.Mem.InputBuffer));
			_drivers.Add(Console = new ConsoleDriver(_fc.Mem.TextBuffer));
			_drivers.Add(Menu = new MenuDriver(Input, Console));
		}

		public FileSystemDriver FS { get; }

		public InputDriver Input { get; }

		public ConsoleDriver Console { get; }

		public MenuDriver Menu { get; }

		public string ActiveGameName => _fc.Mem.ActiveGame;

		public override void Update(double deltaTimeMS)
		{
			foreach (var driver in _drivers)
			{
				driver.Update(deltaTimeMS);
			}
		}

		public void RunProgram(Program prog)
		{
			_fc.PushProgram(prog);
		}

		public void SwapProgram(Program prog)
		{
			_fc.PopProgram(null);
			_fc.PushProgram(prog);
		}

		public void ExitProgram(object returnParam = null)
		{
			_fc.PopProgram(returnParam);
		}

		public void LoadGame(string name, byte[] data = null)
		{
			_fc.Mem.ActiveGame = name;
			_fc.Mem.SpriteBuffer.Clear();
			_fc.Mem.CodeBuffer.Clear();
			if (data != null)
			{
				// TODO: Parse data load into Sprite & Code spaces
			}
		}

		public void OpenGameEditor()
		{
			_fc.Mem.EditMode = true;
		}
	}
}

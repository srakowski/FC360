﻿namespace FC360.Core.Drivers
{
	using IronPython.Hosting;
	using System;
	using System.Text;

	public class Game
	{
		private Action _init;
		private Action<double> _update;
		private Action _draw;

		internal Game(Action init, Action<double> update, Action draw)
		{
			_init = init;
			_update = update;
			_draw = draw;
		}

		public void Init() => _init();
		public void Update(double deltaInMS) => _update(deltaInMS);
		public void Draw() => _draw();
	}

	public class GameDriver : Driver
	{
		private SysDriver _sys;
		private Memory _mem;

		public GameDriver(SysDriver sys, Memory mem)
		{
			_sys = sys;
			_mem = mem;
		}

		public void Load(string name, byte[] data = null)
		{
			_mem.ActiveGameName = name;
			_mem.SpriteBuffer.Clear();
			_mem.CodeBuffer = string.Empty;
			if (data != null)
			{
				_mem.CodeBuffer = Encoding.ASCII.GetString(data);
			}
		}

		public Game Execute()
		{
			var engine = Python.CreateEngine();
			var scope = engine.CreateScope();
			scope.SetVariable("clear", new Action(_sys.Console.Clear));
			scope.SetVariable("output", new Action<int, int, string, bool>(_sys.Console.Output));

			engine.Execute(_mem.CodeBuffer, scope);
			var init = scope.GetVariable("init");
			var update = scope.GetVariable("update");
			var draw = scope.GetVariable("draw");
			return new Game(
				() => init(),
				d => update(d),
				() => draw()
			);
		}

		public void Save()
		{
			var code = Encoding.ASCII.GetBytes(_mem.CodeBuffer);
			_sys.FS.WriteFile(_mem.ActiveGameName, code);
		}
	}
}

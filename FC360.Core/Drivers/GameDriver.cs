namespace FC360.Core.Drivers
{
	using IronPython.Hosting;
	using Microsoft.Scripting.Hosting;
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
		private ScriptEngine _engine;
		private SysDriver _sys;
		private Memory _mem;

		public GameDriver(SysDriver sys, Memory mem)
		{
			_engine = Python.CreateEngine();
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
			var scope = _engine.CreateScope();

			_engine.Execute(
				"import clr\n" +
				"from System import Array\n" +
				"clr.AddReference('FC360.Core')\n" +
				"from FC360.Core.Drivers import Menu, Tab, MenuOption, MenuSelection\n",
				scope
			);

			scope.SetVariable("clear", new Action(_sys.Console.Clear));
			scope.SetVariable("output", new Action<int, int, string, bool>(_sys.Console.Output));
			scope.SetVariable("menu_update", new Func<Menu, MenuSelection>(_sys.Menu.Update));
			scope.SetVariable("menu_draw", new Action<Menu>(_sys.Menu.Draw));

			_engine.CreateScriptSourceFromString(_mem.CodeBuffer)
				.Execute(scope);

			var init = scope.TryGetVariable<Action>("init", out var initFn)
				? initFn
				: new Action(() => { });

			var update = scope.TryGetVariable<Action<double>>("update", out var updateFn)
				? updateFn
				: new Action<double>(_ => { });

			var draw = scope.TryGetVariable<Action>("draw", out var drawFn)
				? drawFn
				: new Action(() => { });

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

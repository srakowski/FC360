namespace FC360.Core.Drivers
{
	using IronPython.Hosting;
	using Microsoft.Scripting.Hosting;
	using System;
	using System.Linq;
	using System.Text;
	using System.Text.Json;

	public record GameData(byte[][] Sprites, string Code);

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

			scope.SetVariable("clear_text", new Action(_sys.Console.Clear));
			scope.SetVariable("output", new Action<int, int, string, bool>(_sys.Console.Output));
			scope.SetVariable("menu_update", new Func<Menu, MenuSelection>(_sys.Menu.Update));
			scope.SetVariable("menu_draw", new Action<Menu>(_sys.Menu.Draw));
			scope.SetVariable("spr", new Action<byte, int, int>(_sys.Graphics.DrawSprite));
			scope.SetVariable("clear_graph", new Action(_sys.Graphics.Clear));
			scope.SetVariable("graphics_mode", new Action(_sys.Graphics.GraphicsMode));
			scope.SetVariable("text_mode", new Action(_sys.Graphics.TextMode));

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

		public void Load(string name)
		{
			_mem.ActiveGameName = name;
			_mem.SpriteBuffer.Clear();
			_mem.CodeBuffer = string.Empty;

			var rawGameData = _sys.FS.ReadFile(name);
			if (rawGameData is null || rawGameData.Length == 0)
			{
				return;
			}

			var serializedGameData = Encoding.ASCII.GetString(rawGameData);
			var gameData = JsonSerializer.Deserialize<GameData>(serializedGameData);
			_mem.CodeBuffer = gameData.Code;
			for (int i = 0; i < gameData.Sprites.Length; i++)
			{
				_mem.SpriteBuffer[(byte)i] = new Sprite(gameData.Sprites[i]);
			}
		}

		public void Save()
		{
			var spriteData = _mem
				.SpriteBuffer
				.Sprites
				.Select(s => s.ToArray())
				.ToArray();

			var gameData = new GameData(spriteData, _mem.CodeBuffer);
			var serializedGameData = JsonSerializer.Serialize(gameData);
			var rawGameData = Encoding.ASCII.GetBytes(serializedGameData);
			_sys.FS.WriteFile(_mem.ActiveGameName, rawGameData);
		}
	}
}

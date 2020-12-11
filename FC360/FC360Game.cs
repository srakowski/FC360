namespace FC360
{
	using FC360.Core;
	using FC360.EditorApi;
	using Microsoft.AspNetCore.Hosting;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;
	using System.Diagnostics;
	using System.Runtime.InteropServices;
	using FCButtonState = Core.ButtonState;

	public class FC360Game : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Texture2D _renderTarget;
		private Color[] _pixelData;
		private FantasyConsole _fc;
		private ViewportAdapter _vpa;
		private bool _editModeStarted;

		public FC360Game()
		{
			_graphics = new GraphicsDeviceManager(this);
			Window.AllowUserResizing = true;
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			base.Initialize();

			_fc = new FantasyConsole();
			_fc.PowerOn();

			_vpa = new ViewportAdapter(
				Window,
				GraphicsDevice,
				_fc.Mem.DisplayBuffer.Width,
				_fc.Mem.DisplayBuffer.Height);

			_renderTarget = new Texture2D(GraphicsDevice,
				_fc.Mem.DisplayBuffer.Width,
				_fc.Mem.DisplayBuffer.Height);

			_pixelData = new Color[_fc.Mem.DisplayBuffer.Width * _fc.Mem.DisplayBuffer.Height];

			Window.TextInput += Window_TextInput;

			var url = "http://localhost:8080";
			Host.CreateDefaultBuilder(new string[] { })
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.UseUrls(url);
					webBuilder.ConfigureServices(s =>
					{
						s.AddSingleton(_ => _fc);
					});
				})
				.RunConsoleAsync();

			// Testing implementation, TODO: delete
			//_fc.Mem.DisplayBuffer[20, 20] = 7;
			//_fc.Mem.DisplayBuffer[10, 10] = 15;
		}

		private void Window_TextInput(object sender, TextInputEventArgs e)
		{
			_fc.Mem.InputBuffer.Text.Append(e.Character);
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			var currKBState = Keyboard.GetState();

			_fc.Mem.InputBuffer.Previous = _fc.Mem.InputBuffer.Current;
			_fc.Mem.InputBuffer.Current = new InputState(
				up: KeyStateToFCButtonState(currKBState, Keys.Up),
				down: KeyStateToFCButtonState(currKBState, Keys.Down),
				left: KeyStateToFCButtonState(currKBState, Keys.Left),
				right: KeyStateToFCButtonState(currKBState, Keys.Right),
				enter: KeyStateToFCButtonState(currKBState, Keys.Enter),
				escape: KeyStateToFCButtonState(currKBState, Keys.Escape)
				);

			_fc.Tick(gameTime.ElapsedGameTime.TotalMilliseconds);

			_fc.Mem.InputBuffer.Text.Clear();

			if (_fc.Mem.EditMode && !_editModeStarted)
			{
				var url = "http://localhost:8080/api/fc360";
				LaunchBrowser(url);
			}
			_editModeStarted = _fc.Mem.EditMode;

			if (!_fc.IsRunning)
			{
				Exit();
			}
		}

		private static FCButtonState KeyStateToFCButtonState(KeyboardState keyboardState, Keys key)
		{
			return keyboardState.IsKeyDown(key) ? FCButtonState.Down : FCButtonState.Up;
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			var width = _fc.Mem.DisplayBuffer.Width;
			for (var y = 0; y < _fc.Mem.DisplayBuffer.Height; y++)
			{
				for (var x = 0; x < width; x++)
				{
					var c = _fc.Mem.Pallete[_fc.Mem.DisplayBuffer[x, y]];
					_pixelData[x + (y * width)] = new Color(c.R, c.G, c.B);
				}
			}
			_renderTarget.SetData(_pixelData);

			_vpa.Reset();
			_spriteBatch.Begin(transformMatrix: _vpa.GetScaleMatrix(), samplerState: SamplerState.PointClamp);
			_spriteBatch.Draw(_renderTarget, Vector2.Zero, Color.White);
			_spriteBatch.End();
		}

		private static void LaunchBrowser(string url)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				Process.Start("xdg-open", url);
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				Process.Start("open", url);
			}
			else
			{
				// throw 
			}
		}
	}
}

namespace FC360
{
	using FC360.Core;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	public class FC360Game : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Texture2D _renderTarget;
		private Color[] _pixelData;
		private FantasyConsole _fc;
		private ViewportAdapter _vpa;

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

			var text = "Hello World!";
			for (var i = 0; i < text.Length; i++)
			{
				_fc.Mem.TextBuffer[i, 0] = new CharCell(text[i], CharCellFlag.Invert);
			}

			// Testing implementation, TODO: delete
			//_fc.Mem.DisplayBuffer[20, 20] = 7;
			//_fc.Mem.DisplayBuffer[10, 10] = 15;

			//			var engine = Python.CreateEngine();
			//			var scope = engine.CreateScope();
			//			engine.Execute(
			//@"def Init(val):
			//	print 'Hello World!' + str(val)
			//", scope);
			//			var init = scope.GetVariable("Init");
			//			init(10);
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			_fc.Tick(gameTime.ElapsedGameTime.TotalMilliseconds);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

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
	}
}

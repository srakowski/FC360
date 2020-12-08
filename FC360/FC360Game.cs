namespace FC360
{
	using FC360.Core;
	using Microsoft.Xna.Framework;
	using Microsoft.Xna.Framework.Graphics;
	using Microsoft.Xna.Framework.Input;

	public class FC360Game : Game
	{
		private const int DisplayWidth = 256;
		private const int DisplayHeight = 224;

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private Texture2D _renderTarget;
		private Color[] _pixelData;
		private Memory _mem;
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

			_mem = new Memory(DisplayWidth, DisplayHeight);

			_vpa = new ViewportAdapter(
				Window,
				GraphicsDevice,
				_mem.DisplayBuffer.Width,
				_mem.DisplayBuffer.Height);

			_renderTarget = new Texture2D(GraphicsDevice,
				_mem.DisplayBuffer.Width,
				_mem.DisplayBuffer.Height);

			_pixelData = new Color[_mem.DisplayBuffer.Width * _mem.DisplayBuffer.Height];

			// Testing implementation, TODO: delete
			_mem.DisplayBuffer[20, 20] = 7;
			_mem.DisplayBuffer[10, 10] = 15;
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			var width = _mem.DisplayBuffer.Width;
			for (var y = 0; y < _mem.DisplayBuffer.Height; y++)
			{ 
				for (var x = 0; x < width; x++)
				{
					var c = _mem.Pallete[_mem.DisplayBuffer[x, y]];
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

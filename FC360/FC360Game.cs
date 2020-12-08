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
		private Memory _mem;

		public FC360Game()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_mem = new Memory();
		}

		protected override void Initialize()
		{
			base.Initialize();
			_renderTarget = new Texture2D(GraphicsDevice,
				_mem.DisplayBuffer.Width,
				_mem.DisplayBuffer.Height);

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
			GraphicsDevice.Clear(Color.CornflowerBlue);

			var width = _renderTarget.Width;
			var pixelData = new Color[width * _renderTarget.Height];
			for (var y = 0; y < _mem.DisplayBuffer.Height; y++)
			{ 
				for (var x = 0; x < _mem.DisplayBuffer.Width; x++)
				{
					var c = _mem.Pallete[_mem.DisplayBuffer[x, y]];
					pixelData[x + (y * width)] = new Color(c.R, c.G, c.B);
				}
			}
			_renderTarget.SetData(pixelData);

			_spriteBatch.Begin();
			_spriteBatch.Draw(_renderTarget, Vector2.Zero, Color.White);
			_spriteBatch.End();
		}
	}
}

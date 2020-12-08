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
		private Memory _memory;
		private Color[] _pallete;

		public FC360Game()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			_memory = new Memory();
			_pallete = new Color[]
			{
				Color.Black,
				Color.Gray,
				Color.White
			};
		}

		protected override void Initialize()
		{
			base.Initialize();
			_renderTarget = new Texture2D(GraphicsDevice,
				_memory.DisplayBuffer.Width,
				_memory.DisplayBuffer.Height);

			_memory.DisplayBuffer[20, 20] = 1;
			_memory.DisplayBuffer[10, 10] = 2;
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
			for (var y = 0; y < _memory.DisplayBuffer.Height; y++)
			{ 
				for (var x = 0; x < _memory.DisplayBuffer.Width; x++)
				{
					pixelData[x + (y * width)] = _pallete[_memory.DisplayBuffer[x, y] % _pallete.Length];
				}
			}
			_renderTarget.SetData(pixelData);

			_spriteBatch.Begin();
			_spriteBatch.Draw(_renderTarget, Vector2.Zero, Color.White);
			_spriteBatch.End();
		}
	}
}

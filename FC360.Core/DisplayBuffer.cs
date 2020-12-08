namespace FC360.Core
{
	public class DisplayBuffer
	{
		private byte[,] _data;

		public DisplayBuffer(int width, int height)
		{
			_data = new byte[width, height];
		}

		public byte this[int x, int y]
		{
			get => _data[x, y];
			set => _data[x, y] = (byte)(value % 16);
		}

		public int Width => _data.GetLength(0);

		public int Height => _data.GetLength(1);
	}
}

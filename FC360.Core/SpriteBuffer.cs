namespace FC360.Core
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public struct Sprite : IEnumerable<byte>
	{
		public const int Width = 8;
		public const int Height = 8;

		private byte[,] _data;

		public Sprite(byte[] data)
		{
			_data = new byte[Height, Width];
			for (var i = 0; i < data.Length && i < _data.LongLength; i++)
			{
				var y = i / Height;
				var x = i % Width;
				_data[y, x] = data[i];
			}
		}

		public byte this[int x, int y] => _data is null ? 0 : _data[y, x];

		public IEnumerator<byte> GetEnumerator()
		{
			for (var y = 0; y < Height; y++)
			{
				for (var x = 0; x < Width; x++)
				{
					yield return this[x, y];
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public class SpriteBuffer
	{
		private Sprite[] _data;

		public SpriteBuffer()
		{
			_data = new Sprite[256];
		}

		public IEnumerable<Sprite> Sprites => _data;

		public Sprite this[byte i]
		{
			get => _data[i];
			set => _data[i] = value;
		}

		public void Clear()
		{
			Array.Clear(_data, 0, _data.Length);
		}
	}
}

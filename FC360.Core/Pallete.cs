namespace FC360.Core
{
	using System.Linq;

	public class Pallete
	{
		private readonly RgbColor[] _colors;

		public Pallete()
		{
			// TODO: get input for what these colors should be, grayscale for now
			_colors = Enumerable.Range(0, 16)
				.Select(i => (byte)(i * (256 / 16)))
				.Select(v => new RgbColor(v, v, v))
				.ToArray();
		}

		public RgbColor this[int i] => _colors[i];
	}
}

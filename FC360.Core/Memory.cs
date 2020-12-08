namespace FC360.Core
{
	public class Memory
	{
		public Memory(int width, int height)
		{
			Pallete = new Pallete();
			DisplayBuffer = new DisplayBuffer(width, height);
		}

		public Pallete Pallete { get; }
		public DisplayBuffer DisplayBuffer { get; }
	}
}

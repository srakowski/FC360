namespace FC360.Core
{
	public class Memory
	{
		public Memory()
		{
			Pallete = new Pallete();
			DisplayBuffer = new DisplayBuffer(256, 224);
		}

		public Pallete Pallete { get; }
		public DisplayBuffer DisplayBuffer { get; }
	}
}

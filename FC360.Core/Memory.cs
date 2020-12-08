namespace FC360.Core
{
	public class Memory
	{
		public Memory()
		{
			DisplayBuffer = new DisplayBuffer(256, 224);
		}

		public DisplayBuffer DisplayBuffer { get; }
	}
}

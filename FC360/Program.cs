namespace FC360
{
	using System;

	public static class Program
	{
		[STAThread]
		static void Main()
		{
			using (var game = new FC360Game())
				game.Run();
		}
	}
}

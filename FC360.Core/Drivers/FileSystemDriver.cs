namespace FC360.Core.Drivers
{
	using System.IO;
	using System.Linq;

	public class FileSystemDriver : Driver
	{
		private const string ext = ".fcgame";

		public FileSystemDriver() { }

		public string[] GetFiles()
		{
			return Directory.GetFiles(".", $"*{ext}")
				.Select(Path.GetFileNameWithoutExtension)
				.ToArray();
		}

		public byte[] ReadFile(string fileName)
		{
			return File.ReadAllBytes($"{fileName}{ext}");
		}

		public void WriteFile(string fileName, byte[] data)
		{
			File.WriteAllBytes($"{fileName}{ext}", data);
		}
	}
}
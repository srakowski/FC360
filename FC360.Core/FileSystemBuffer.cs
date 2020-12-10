namespace FC360.Core
{
	public enum FileSystemInterruptCode : byte
	{
		Noop,
		GetFiles,
		WriteFile,
		ReadFile,
	}

	public struct FileSystemInterrupt
	{
		public FileSystemInterruptCode Code;
		public string FileName;
		public short BufferIdx;
		public short BufferLen;
		public bool Complete;
	}

	public class FileSystemBuffer
	{
		public FileSystemInterrupt[] _interruptTable;
		private string[] _data;

		public FileSystemBuffer()
		{
			_interruptTable = new FileSystemInterrupt[16];
			// TODO: this will break after more than 128 files!
			_data = new string[128];
		}

		public FileSystemInterrupt[] InterruptTable => _interruptTable;

		public int Interrupt(
			FileSystemInterruptCode code,
			string fileName = null,
			string[] data = null
			)
		{
			if (data != null)
			{
				for (var i = 0; i < data.Length; i++)
				{
					_data[i] = data[i];
				}
			}

			_interruptTable[0] = new FileSystemInterrupt
			{
				Code = code,
				FileName = fileName,
				BufferIdx = 0,
				BufferLen = (short)(data?.Length ?? 0),
				Complete = false
			};
			return 0;
		}

		public FileSystemInterrupt GetInterrupt(int idx)
		{
			return _interruptTable[idx];
		}

		public void ClearInterrupt(int idx)
		{
			_interruptTable[idx] = new FileSystemInterrupt();
		}

		public string[] GetData(int idx, int len)
		{
			// TODO: does this copy the strings? will break if overwritten?
			return _data[idx..(idx + len)];
		}

		public void CompleteInterupt(int idx, string[] data)
		{
			for (var i = 0; i < data.Length; i++)
			{
				_data[i] = data[i];
			}
			_interruptTable[idx].Complete = true;
			_interruptTable[idx].BufferIdx = 0;
			_interruptTable[idx].BufferLen = (short)data.Length;
		}
	}
}
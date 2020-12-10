namespace FC360.Core.Drivers
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	public class FileSystemDriver : Driver
	{
		private FileSystemBuffer _fsBuffer;
		private List<IEnumerator> _coroutines;

		public FileSystemDriver(FileSystemBuffer fsBuffer)
		{
			_fsBuffer = fsBuffer;
			_coroutines = new List<IEnumerator>();
		}

		public Promise<string[]> GetFiles()
		{
			return new Promise<string[]>(resolve =>
				_coroutines.Add(GetFileList(resolve))
			);
		}

		public Promise<byte[]> ReadFile(string gameFileName)
		{
			return new Promise<byte[]>(resolve =>
			{
				// TODO: call sync process and register callback?
				resolve(new byte[] { });
			});
		}

		public override void Update(double deltaTimeMS)
		{
			_coroutines
				.RemoveAll(cr => !cr.MoveNext());
		}

		private IEnumerator GetFileList(Action<string[]> resolve)
		{
			var idx = _fsBuffer.Interrupt(FileSystemInterruptCode.GetFiles);
			yield return null;
			while (true)
			{
				var interrupt = _fsBuffer.GetInterrupt(idx);
				if (interrupt.Complete)
				{
					var files = _fsBuffer.GetData(interrupt.BufferIdx, interrupt.BufferLen);
					resolve(files);
					_fsBuffer.ClearInterrupt(idx);
					break;
				}
				yield return null;
			}
		}
	}
}
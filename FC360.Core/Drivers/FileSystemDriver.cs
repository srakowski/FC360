namespace FC360.Core.Drivers
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

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
				_coroutines.Add(ExecuteGetFiles(resolve))
			);
		}

		public Promise<byte[]> ReadFile(string gameFileName)
		{
			return new Promise<byte[]>(resolve =>
			{
				_coroutines.Add(ExecuteReadFile(gameFileName, resolve));
			});
		}

		public Promise<string> WriteFile(string gameFileName, byte[] data)
		{
			return new Promise<string>(resolve =>
			{
				_coroutines.Add(ExecuteWriteFile(gameFileName, data, resolve));
			});
		}

		public override void Update(double deltaTimeMS)
		{
			_coroutines
				.RemoveAll(cr => !cr.MoveNext());
		}

		private IEnumerator ExecuteGetFiles(Action<string[]> resolve)
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

		private IEnumerator ExecuteReadFile(string gameFileName, Action<byte[]> resolve)
		{
			var idx = _fsBuffer.Interrupt(
				FileSystemInterruptCode.ReadFile,
				fileName: gameFileName);
			yield return null;
			while (true)
			{
				var interrupt = _fsBuffer.GetInterrupt(idx);
				if (interrupt.Complete)
				{
					var files = _fsBuffer.GetData(interrupt.BufferIdx, interrupt.BufferLen).First();
					resolve(Encoding.ASCII.GetBytes(files));
					_fsBuffer.ClearInterrupt(idx);
					break;
				}
				yield return null;
			}
		}

		private IEnumerator ExecuteWriteFile(string gameFileName, byte[] data, Action<string> resolve)
		{
			var idx = _fsBuffer.Interrupt(
				FileSystemInterruptCode.WriteFile,
				fileName: gameFileName,
				new string[] { Encoding.ASCII.GetString(data) });
			yield return null;
			while (true)
			{
				var interrupt = _fsBuffer.GetInterrupt(idx);
				if (interrupt.Complete)
				{
					resolve(gameFileName);
					break;
				}
				yield return null;
			}
		}
	}
}
namespace FC360.Core.Drivers
{
	using System.Collections.Generic;

	public class InputDriver : Driver
	{
		private readonly InputBuffer _inputBuffer;

		public InputDriver(InputBuffer inputBuffer)
		{
			_inputBuffer = inputBuffer;
		}

		/// <summary>
		/// Button was previously in a Down state, but is now in an Up state.
		/// </summary>
		/// <param name="button"></param>
		/// <returns></returns>
		public bool ButtonWasPressed(Button button)
		{
			var prevState = _inputBuffer.Previous[button];
			var currState = _inputBuffer.Current[button];
			return prevState == ButtonState.Down &&
				currState == ButtonState.Up;
		}

		public IEnumerable<char> GetTextInput()
		{
			return _inputBuffer.Text;
		}
	}
}
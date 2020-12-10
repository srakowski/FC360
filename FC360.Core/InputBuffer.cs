using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FC360.Core
{
	public enum Button : byte
	{
		Up,
		Down,
		Left,
		Right,
		Enter
	}

	public enum ButtonState : byte
	{
		Up,
		Down,
	}

	/// <summary>
	/// TODO: should this be an bitarray? consider refactoring
	/// </summary>
	public struct InputState
	{
		private ButtonState[] _buttonStates;

		public InputState(
			ButtonState up,
			ButtonState down,
			ButtonState left,
			ButtonState right,
			ButtonState enter)
		{
			_buttonStates = new ButtonState[Enum.GetValues(typeof(Button)).Length];
			_buttonStates[(int)Button.Up] = up;
			_buttonStates[(int)Button.Down] = down;
			_buttonStates[(int)Button.Left] = left;
			_buttonStates[(int)Button.Right] = right;
			_buttonStates[(int)Button.Enter] = enter;
		}

		public ButtonState this[Button button] => GetButtonState(button);

		public ButtonState GetButtonState(Button button)
		{
			return _buttonStates is null ? ButtonState.Up : _buttonStates[(int)button];
		}
	}

	public class TextInputBuffer : IEnumerable<char>
	{
		private readonly char[] _data;
		private int _idx;

		public TextInputBuffer()
		{
			_data = new char[64];
		}

		public void Append(char value)
		{
			_data[_idx % _data.Length] = value;
			_idx++;
		}

		public void Clear()
		{
			Array.Clear(_data, 0, _data.Length);
			_idx = 0;
		}

		public IEnumerator<char> GetEnumerator()
		{
			for (var i = 0; i < _idx; i++)
				yield return _data[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}

	public class InputBuffer
	{
		public InputState Current { get; set; }

		public InputState Previous { get; set; }

		public TextInputBuffer Text { get; } = new TextInputBuffer();
	}
}

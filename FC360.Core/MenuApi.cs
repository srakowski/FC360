namespace FC360.Core
{
	using System.Collections.Generic;

	public struct MenuSelection
	{
		public MenuSelection(int tabIdx, int menuOptionIdx)
		{
			HasValue = tabIdx >= 0 && menuOptionIdx >= 0;
			TabIdx = tabIdx;
			MenuOptionIdx = menuOptionIdx;
		}

		public bool HasValue { get; }
		public int TabIdx { get; }
		public int MenuOptionIdx { get; }

		internal static MenuSelection None() => new MenuSelection(-1, -1);
	}

	public class MenuOption
	{
		public MenuOption(string name)
		{
			Name = name;
		}

		public string Name { get; }
	}

	public class Tab
	{
		private readonly MenuOption[] _menuOptions;
		private int _selectedOptionIdx;

		public Tab(string title, params MenuOption[] menuOptions)
		{
			Title = title;
			_menuOptions = menuOptions;
			_selectedOptionIdx = 0;
		}

		public string Title { get; }

		public IEnumerable<MenuOption> MenuOptions => _menuOptions;

		internal int SelectedMenuOptionIdx => _selectedOptionIdx;

		internal MenuOption SelectedMenuOption => _menuOptions[_selectedOptionIdx];

		internal void SelectUp()
		{
			_selectedOptionIdx--;
			_selectedOptionIdx = _selectedOptionIdx < 0 ? _menuOptions.Length - 1 : _selectedOptionIdx;
		}

		internal void SelectDown()
		{
			_selectedOptionIdx++;
			_selectedOptionIdx %= _menuOptions.Length;
		}
	}

	public class Menu
	{
		private readonly Tab[] _tabs;
		private int _selectedTabIndex;

		public Menu(string title, params Tab[] tabs)
		{
			Title = title;
			_tabs = tabs;
			_selectedTabIndex = 0;
		}

		public string Title { get; }

		public IEnumerable<Tab> Tabs => _tabs;

		internal int SelectedTabIdx => _selectedTabIndex;

		internal Tab SelectedTab => _tabs[_selectedTabIndex];

		internal void SelectLeft()
		{
			_selectedTabIndex--;
			_selectedTabIndex = _selectedTabIndex < 0 ? _tabs.Length - 1 : _selectedTabIndex;
		}

		internal void SelectRight()
		{
			_selectedTabIndex++;
			_selectedTabIndex %= _tabs.Length;
		}
	}

	public class MenuApi
	{
		private readonly InputApi _inputApi;
		private readonly TextApi _textApi;

		public MenuApi(InputApi inputApi, TextApi textApi)
		{
			_inputApi = inputApi;
			_textApi = textApi;
		}

		public MenuSelection Update(Menu menu)
		{
			if (_inputApi.ButtonWasPressed(Button.Left))
			{
				menu.SelectLeft();
			}
			else if(_inputApi.ButtonWasPressed(Button.Right))
			{
				menu.SelectRight();
			}

			if (_inputApi.ButtonWasPressed(Button.Up))
			{
				menu.SelectedTab.SelectUp();
			}
			else if (_inputApi.ButtonWasPressed(Button.Down))
			{
				menu.SelectedTab.SelectDown();
			}

			if (_inputApi.ButtonWasPressed(Button.Enter))
			{
				return new MenuSelection(
					menu.SelectedTabIdx,
					menu.SelectedTab.SelectedMenuOptionIdx
					);
			}

			return MenuSelection.None();
		}

		public void Draw(Menu menu)
		{
			_textApi.Clear();

			_textApi.Output(0, 0, menu.Title);
			_textApi.InvertRange(0, 0, _textApi.BufferWidth, 1);

			var cursorX = 0;
			var cursorY = 1;

			foreach (var tab in menu.Tabs)
			{
				_textApi.Output(cursorX, cursorY, tab.Title, tab == menu.SelectedTab);
				cursorX += tab.Title.Length + 1;
			}

			cursorX = 0;
			cursorY++;

			var i = 0; 
			foreach (var menuOption in menu.SelectedTab.MenuOptions)
			{
				var optionText = $"{i + 1}:";
				_textApi.Output(cursorX, cursorY, optionText, invert: menuOption == menu.SelectedTab.SelectedMenuOption);
				cursorX += optionText.Length;
				_textApi.Output(cursorX, cursorY, menuOption.Name);
				i++;
				cursorY++;
				cursorX = 0;
			}
		}
	}
}

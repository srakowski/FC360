namespace FC360.Core
{
	using System.Collections.Generic;

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
		private MenuOption[] _menuOptions;

		public Tab(string title, params MenuOption[] menuOptions)
		{
			Title = title;
			_menuOptions = menuOptions;
			SelectedMenuOption = menuOptions[0];
		}

		public string Title { get; }

		public IEnumerable<MenuOption> MenuOptions => _menuOptions;

		public MenuOption SelectedMenuOption { get; private set; }
	}

	public class Menu
	{
		public Menu(string title, params Tab[] tabs)
		{
			Title = title;
			Tabs = tabs;
			SelectedTab = tabs[0];
		}

		public string Title { get; }

		public IEnumerable<Tab> Tabs { get; }

		public Tab SelectedTab { get; private set; }
	}

	public class MenuApi
	{
		private TextApi _textApi;

		public MenuApi(TextApi textApi)
		{
			_textApi = textApi;
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
				var optionText = $"{i}:";
				_textApi.Output(cursorX, cursorY, optionText, invert: menuOption == menu.SelectedTab.SelectedMenuOption);
				cursorX += optionText.Length;
				_textApi.Output(cursorX, cursorY, menuOption.Name);
				i++;
				cursorY++;
			}
		}
	}
}

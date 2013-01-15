using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharedClasses;
using System.Reflection;

namespace SettingsInterop
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		PropertiesEditor pe;

		public MainWindow()
		{
			InitializeComponent();
		}

		private bool alreadyShowingPropertiesEditor = false;
		private void ShowSettingsEditor()
		{
			if (alreadyShowingPropertiesEditor)
				return;
			alreadyShowingPropertiesEditor = true;

			try
			{
				if (!SettingsSimple.UseOnlineListAndSaveIfChanged(
					(objList) =>
					{
						if (pe == null)
							pe = new PropertiesEditor(objList);
						else
							pe.PopulateList(objList);
						pe.ShowDialog();
						pe = null;
					}))
					return;
			}
			finally
			{
				alreadyShowingPropertiesEditor = false;
			}
		}

		private void OnNotificationArayIconMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (pe != null && pe.IsVisible)
				pe.Close();
			else
				ShowSettingsEditor();
		}

		private void OnMenuItemShowClick(object sender, EventArgs e)
		{
			ShowSettingsEditor();
		}

		private void OnMenuItemExitClick(object sender, EventArgs e)
		{
			if (pe != null && pe.IsVisible)
				pe.Close();
			this.Close();
		}

		private void OnMenuItemAboutClick(object sender, EventArgs e)
		{
			AboutWindow2.ShowAboutWindow(new System.Collections.ObjectModel.ObservableCollection<DisplayItem>()
			{
				new DisplayItem("Author", "Francois Hill"),
				new DisplayItem("Icon(s) obtained from", null)
			});
		}
	}
}

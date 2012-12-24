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

		private void ShowSettingsEditor()
		{
			List<object> objList;
			Dictionary<IInterceptorNotifiable, Dictionary<PropertyInfo, object>> objectsAndPropertyValues;
			if (!SettingsSimple.GetListOfOnlineSettings(out objList, out objectsAndPropertyValues))
				return;//Exit if could not get list

			if (pe == null)
				pe = new PropertiesEditor(objList);
			else
				pe.PopulateList(objList);
			pe.ShowDialog();
			pe = null;
			objList.Clear();
			objList = null;

			SettingsSimple.ProcessPropertyCompareToPrevious(objectsAndPropertyValues);
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
			this.Close();
		}
	}
}

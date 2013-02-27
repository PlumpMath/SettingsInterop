using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SharedClasses;

namespace SettingsInterop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			System.Windows.Forms.Application.EnableVisualStyles();

			base.OnStartup(e);

			SharedClasses.AutoUpdating.CheckForUpdates_ExceptionHandler();
			//SharedClasses.AutoUpdating.CheckForUpdates(null, null, true);

			SettingsInterop.MainWindow mw = new MainWindow();
			mw.ShowDialog();
		}
	}
}

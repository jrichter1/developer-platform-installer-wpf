/*
 * Created by SharpDevelop.
 * User: jbossqa
 * Date: 10/23/2015
 * Time: 14:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace wpf_installer
{
	/// <summary>
	/// Description of PageHandler.
	/// </summary>
	public class PageHandler
	{
		private static Window1 mainWindow;
		
		public PageHandler(Window1 window)
		{
			mainWindow = window;
		}
		
		public void SwitchPage(Page page) {
			mainWindow.Navigate(page);
		}
	}
}

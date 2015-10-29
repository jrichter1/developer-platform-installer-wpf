/*
 * Created by SharpDevelop.
 * User: jbossqa
 * Date: 10/23/2015
 * Time: 1:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace wpf_installer
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		private Page1 page1;
		private Page2 page2;
		private Page3 page3;
		private Page4 page4;
		
		public Window1()
		{
			InitializeComponent();
			page2 = new Page2(this);
			page1 = new Page1(this, page2);
			Navigate(page1);
		}
		
		public void Navigate(Page page) {
			this.frame.Content = null;
			this.frame.Content = page;			
			
			if (page is Page3) {
				this.next.Visibility = Visibility.Visible;
			} else {
				this.next.Visibility = Visibility.Hidden;
			}
			
			if (page is Page2) {
				this.download.Visibility = Visibility.Visible;
			} else {
				this.download.Visibility = Visibility.Hidden;
			}
		}
		
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			MessageBoxResult result = MessageBox.
				Show("Setup is not complete. If you exit now, the program will not be installed. \n\n Exit setup?", 
				     "Exit Setup", MessageBoxButton.YesNo, MessageBoxImage.Question);
    		if (result == MessageBoxResult.No)
    		{
        		e.Cancel = true;
    		} else {
    			System.Environment.Exit(0);
    		}
		}
		
		public void ToggleNextButton(bool enabled) {
			this.next.IsEnabled = enabled;
		}
		
		public void ToggleDownloadButton(bool enabled) {
			this.download.IsEnabled = enabled;
		}
		
		void button1_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		void download_Click(object sender, RoutedEventArgs e)
		{			
			page3 = new Page3(page2.GetToolsToInstall(), this);
			Navigate(page3);
		}
		void next_Click(object sender, RoutedEventArgs e)
		{
			page4 = new Page4(this);
			Navigate(page4);
		}
	}
}
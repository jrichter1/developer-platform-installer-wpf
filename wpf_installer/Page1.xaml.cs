/*
 * Created by SharpDevelop.
 * User: jbossqa
 * Date: 10/23/2015
 * Time: 13:18
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
	/// Interaction logic for Page1.xaml
	/// </summary>
	public partial class Page1 : Page
	{
		private Window1 parent;		
		private Page next;
		
		public Page1(Window1 parent, Page successor)
		{
			InitializeComponent();
			this.parent = parent;
			this.next = successor;
			this.login.Focus();
		}
		
		void label1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			System.Diagnostics.Process.Start("http://www.redhat.com/en");
		}
		
		void button1_Click(object sender, RoutedEventArgs e)
		{
			if (login.Text.Length > 0 && password.Password.Length > 0) {
				parent.Navigate(next);
			} else {
				this.error.Visibility = Visibility.Visible;
			}
		}
		void login_GotFocus(object sender, RoutedEventArgs e)
		{
			this.error.Visibility = Visibility.Hidden;
		}
	}
}
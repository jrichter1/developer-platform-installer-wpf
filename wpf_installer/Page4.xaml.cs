/*
 * Created by SharpDevelop.
 * User: jbossqa
 * Date: 10/26/2015
 * Time: 16:29
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
	/// Interaction logic for Page4.xaml
	/// </summary>
	public partial class Page4 : Page
	{
		private Window1 parent;
		
		public Page4(Window1 parent)
		{
			this.parent = parent;
			InitializeComponent();
		}
		void button1_Click(object sender, RoutedEventArgs e)
		{
			Environment.Exit(0);
		}
	}
}
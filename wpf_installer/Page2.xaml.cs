/*
 * Created by SharpDevelop.
 * User: jbossqa
 * Date: 10/23/2015
 * Time: 14:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Forms;

namespace wpf_installer
{
	/// <summary>
	/// Interaction logic for Page2.xaml
	/// </summary>
	public partial class Page2 : Page
	{
		
		private List<Tools> toolsToInstall;
		private Object lockObject = new Object();
		private Window1 parent;
		
		public Page2(Window1 parent)
		{
			this.parent = parent;
			toolsToInstall = new List<Tools>();
			InitializeComponent();
			
			vagrantLabel.Content = "Searching for existing Vagrant installation";
			vboxLabel.Content = "Searching for existing Virtual Box installation";
			jdkLabel.Content = "Searching for existing JDK installation";
			
			Thread t1 = new Thread(() => setPath(Tools.Vagrant, false));
			Thread t2 = new Thread(() => setPath(Tools.Virtual_Box, true));
			Thread t3 = new Thread(() => setPath(Tools.JDK, false));
			
			t1.Start();
			t2.Start();
			t3.Start();
			
			Thread t4 = new Thread(() => {
			    while (t1.IsAlive || t2.IsAlive || t3.IsAlive) {
			                       		Thread.Sleep(500);
				}
			   	this.Dispatcher.Invoke((Action)(() => this.parent.ToggleDownloadButton(true)));
			});
			t4.Start();
		}
		
		public List<Tools> GetToolsToInstall()
		{
			return new List<Tools>(toolsToInstall);
		}
		
		private void setPath(Tools tool, bool available)
		{
			string value = findTool(tool, available);
			
			this.Dispatcher.Invoke((Action)(() => {
				if (value == null) {
					String toolName = tool.ToString().Replace("_", " ");
					String content = toolName + " not found on your system. " + toolName + " will be installed, or";
				
			
					switch (tool) {
						case Tools.Vagrant:
							vagrantLabel.Content = content;
							vagrantLabel.Visibility = Visibility.Visible;
							vagrantLink.Visibility = Visibility.Visible;
							vagrantBrowse.Visibility = Visibility.Hidden;
							vagrantTextBox.Visibility = Visibility.Hidden;
							break;
						case Tools.Virtual_Box:
							vboxLabel.Content = content;
							vboxLabel.Visibility = Visibility.Visible;
							vboxBrowse.Visibility = Visibility.Hidden;
							vboxTextBox.Visibility = Visibility.Hidden;
							break;
						case Tools.JDK:
							jdkLabel.Content = content;
							jdkLabel.Visibility = Visibility.Visible;
							jdkBrowse.Visibility = Visibility.Hidden;
							jdkTextBox.Visibility = Visibility.Hidden;
							break;
					}
				} else {
					switch (tool) {
						case Tools.Vagrant:
							vagrantLabel.Visibility = Visibility.Hidden;
							vagrantLink.Visibility = Visibility.Hidden;
							vagrantBrowse.Visibility = Visibility.Visible;
							vagrantTextBox.Visibility = Visibility.Visible;
							vagrantTextBox.Text = value;
							break;
						case Tools.Virtual_Box:
							vboxLabel.Visibility = Visibility.Hidden;
							vboxBrowse.Visibility = Visibility.Visible;
							vboxTextBox.Visibility = Visibility.Visible;
							vboxTextBox.Text = value;
							break;
						case Tools.JDK:
							jdkLabel.Visibility = Visibility.Hidden;
							jdkBrowse.Visibility = Visibility.Visible;
							jdkTextBox.Visibility = Visibility.Visible;
							jdkTextBox.Text = value;
							break;
					}
				}
					toolsToInstall.Add(tool);
			}));
		}
		
		private string findTool(Tools tool, bool available)
		{
			string path = @"C:\Program Files";
			Thread.Sleep(new Random().Next(200, 3000));
			
			return available ? path : null;
		}
		
		void vagrantBrowse_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			dialog.SelectedPath = vagrantTextBox.Text;
			dialog.ShowDialog();
			
			vagrantTextBox.Text = dialog.SelectedPath;
		}
		void vboxBrowse_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			dialog.SelectedPath = vboxTextBox.Text;
			dialog.ShowDialog();
			
			vboxTextBox.Text = dialog.SelectedPath;
		}
		void jdkBrowse_Click(object sender, RoutedEventArgs e)
		{
		
			FolderBrowserDialog dialog = new FolderBrowserDialog();
			dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			dialog.SelectedPath = jdkTextBox.Text;
			dialog.ShowDialog();
			
			jdkTextBox.Text = dialog.SelectedPath;
		}
		void vagrantLink_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			var dialog = new FolderBrowserDialog();
			dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			dialog.ShowDialog();
			
			if (string.IsNullOrEmpty(dialog.SelectedPath)) {
				return;
			}
			vagrantTextBox.Text = dialog.SelectedPath;
			vagrantTextBox.Visibility = Visibility.Visible;
			vagrantBrowse.Visibility = Visibility.Visible;
			vagrantLabel.Visibility = Visibility.Hidden;
			vagrantLink.Visibility = Visibility.Hidden;
		}
		
	}
	
	public enum Tools
	{
		Vagrant,
		Virtual_Box,
		JDK				
	}
}
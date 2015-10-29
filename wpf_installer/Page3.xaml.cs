/*
 * Created by SharpDevelop.
 * User: jbossqa
 * Date: 10/26/2015
 * Time: 13:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Web;

namespace wpf_installer
{
	/// <summary>
	/// Interaction logic for Page3.xaml
	/// </summary>
	public partial class Page3 : Page
	{
		private List<Tools> tools;
		private Window1 parent;
		
		public Page3(List<Tools> installTools, Window1 parent)
		{
			this.parent = parent;
			tools = new List<Tools>(installTools);
			InitializeComponent();
		}
		
		protected override void OnInitialized(EventArgs e)
		{
			base.OnInitialized(e);
			this.parent.ToggleNextButton(false);
			
			foreach (Tools tool in Enum.GetValues(typeof(Tools))) {
				if (tools.Contains(tool)) {
					installTool(tool);
				}
				configureTool(tool);
			}
		}
		
		private void configureTool(Tools tool)
		{
			
		}
		
		private void installTool(Tools tool)
		{
			Thread thread = new Thread(() => {
				switch (tool) {
					case Tools.JDK:			                        
						string url = "http://cdn.azulsystems.com/zulu/bin/zulu1.8.0_60-8.9.0.4-win64.zip";
						string fileName = "zulu1.8.0_60-8.9.0.4-win64.zip";
						string referer = "http://www.azul.com/downloads/zulu/zulu-windows/";
					
						downloadFile(url, fileName, referer);
						break;
					case Tools.Vagrant:
						break;
					case Tools.Virtual_Box:
						break;
				}
			});
			thread.Start();
		}
		
		private void downloadFile(string url, string fileName, string refferal)
		{
			WebClient client = new WebClient();
			client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
			client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
			if (!string.IsNullOrEmpty(refferal)) {
				client.Headers.Add("Referer", refferal);
			}
			client.DownloadFileAsync(new Uri(url), fileName);
		}
		
		void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			Dispatcher.BeginInvoke((MethodInvoker)delegate {
				double bytesIn = double.Parse(e.BytesReceived.ToString());
				double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
				double percentage = bytesIn / totalBytes * 100;
				progressLabel.Content = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
				progressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
			});
		}
		void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
		{
			Dispatcher.BeginInvoke((MethodInvoker)delegate {
				progressLabel.Content = "Completed";
				this.parent.ToggleNextButton(true);
			}); 
		}
	}
}
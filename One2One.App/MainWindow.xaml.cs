using One2One.Lib.Server;
using One2One.Lib.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Net;

namespace One2One.App
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private NetworkModel networkModel = new NetworkModel();
		

		public MainWindow()
		{
			InitializeComponent();
		}

		private void mainWindow_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void server_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			receiveMessageBox.Dispatcher.Invoke(new Action(() =>
			{
				receiveMessageBox.AppendText(string.Format("Received ({0}): {1}\r", DateTime.Now, e.MessageReceived));
			}));
		}

		private void menuItem_StartStopServer_Click(object sender, RoutedEventArgs e)
		{
			startLocalServer();
		}
        
		private void startLocalServer()
		{            
            if(validateIPv4(networkModel.LocalAddress) == false || validateIPv4(networkModel.ExernalAddress) == false)
            {
                MessageBox.Show("Valid IP Addresses must be set under the 'Network...' setting.");
                return;
            }

			try
			{                
				networkModel.Server = new TcpServer(networkModel.LocalAddress, networkModel.LocalPort);
				networkModel.Server.MessageReceived += new MessageReceivedHandler(server_MessageReceived);
		
				Thread t = new Thread(networkModel.Server.Listen);
				t.IsBackground = true;
				t.Start();

				receiveMessageBox.AppendText(string.Format("<==== Listening on {0}:{1} ====>\n", networkModel.LocalAddress, networkModel.LocalPort));
			}
			catch (Exception ex)
			{
				receiveMessageBox.AppendText(ex.ToString());
			}
		}


        private bool validateIPv4(string ipString)
        {
            //Source: https://stackoverflow.com/questions/11412956/what-is-the-best-way-of-validating-an-ip-address
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

		private void menuItem_Disconnect_Click(object sender, RoutedEventArgs e)
		{
		}


		private void menuItem_Settings_Click(object sender, RoutedEventArgs e)
		{

		}

		private void sendMessageBox_KeyDown(object sender, KeyEventArgs e)
		{

			if (e.Key == Key.Enter)
			{
				if (networkModel.Client == null)
				{
					networkModel.Client = new TcpClient(networkModel.ExernalAddress, networkModel.ExernalPort);
					networkModel.Client.Connect();
				}

				try
				{
					receiveMessageBox.AppendText(string.Format("Sent: {0}\r", sendMessageBox.Text));
					networkModel.Client.Message(sendMessageBox.Text);
				}
				catch (Exception)
				{
					receiveMessageBox.AppendText("Message could not be sent. Please try again.\r");
					networkModel.Client = null; //reset the client object
				}

				sendMessageBox.Clear();

			}
		}

		private void sendMessageBox_GotFocus(object sender, RoutedEventArgs e)
		{
			sendMessageBox.Text = "";
		}

		private void sendMessageBox_LostFocus(object sender, RoutedEventArgs e)
		{
			if (sendMessageBox.Text == "")
				sendMessageBox.Text = "Enter your message and press RETURN to send...";
		}


		private void menuItem_NetworkSettings_Click(object sender, RoutedEventArgs e)
		{
			NetworkSettings s = new NetworkSettings(networkModel);	
					
			s.ShowDialog();
		}



	}
}

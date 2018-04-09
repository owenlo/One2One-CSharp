using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace One2One.App
{
	/// <summary>
	/// Interaction logic for NetworkSettings.xaml
	/// </summary>
	public partial class NetworkSettings : Window
	{

		private NetworkModel _networkModel;

		public NetworkSettings(NetworkModel networkModel)
		{
			InitializeComponent();
			_networkModel = networkModel;
		}

		private void windowNetworkSettings_Loaded(object sender, RoutedEventArgs e)
		{
			txtBoxLocalAddress.Text = _networkModel.LocalAddress;
			txtBoxLocalPort.Text = _networkModel.LocalPort.ToString();
			txtBoxExternalAddress.Text = _networkModel.ExernalAddress;
			txtBoxExternalPort.Text = _networkModel.ExernalPort.ToString();
		}


		private void btnAccept_Click(object sender, RoutedEventArgs e)
		{
			_networkModel.LocalAddress = txtBoxLocalAddress.Text;
			_networkModel.LocalPort = int.Parse(txtBoxLocalPort.Text);
			_networkModel.ExernalAddress = txtBoxExternalAddress.Text;
			_networkModel.ExernalPort = int.Parse(txtBoxExternalPort.Text);

			this.Close();
		}


		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
		txtBoxLocalAddress.Text = "127.0.0.1";
		txtBoxLocalPort.Text = "1234";
		txtBoxExternalAddress.Text = "127.0.0.1";
		txtBoxExternalPort.Text = "4321";
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			txtBoxLocalAddress.Text = "127.0.0.1";
			txtBoxLocalPort.Text = "4321";
			txtBoxExternalAddress.Text = "127.0.0.1";
			txtBoxExternalPort.Text = "1234";
		}



	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace One2One.Lib.Client
{
	public class TcpClient : ITcpClient
	{
		private string _externalAddress;
		private int _externalPort;
		private System.Net.Sockets.TcpClient _tcpClient;

		public TcpClient(string externalAddress, int externalPort)
		{
			_externalAddress = externalAddress;
			_externalPort = externalPort;
		}

		void ITcpClient.Connect()
		{
			try
			{
				// Create a TcpClient. 
				// Note, for this client to work you need to have a TcpServer  
				// connected to the same address as specified by the server, port 
				// combination.				
				if (_tcpClient == null)
				{
					_tcpClient = new System.Net.Sockets.TcpClient(_externalAddress, _externalPort);

				}
			}
			catch (Exception) { }
		}

		void ITcpClient.Message(string message)
		{
			// Translate the passed message into ASCII and store it as a Byte array.
			Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

			// Get a client stream for reading and writing. 
			//  Stream stream = client.GetStream();

			NetworkStream stream = _tcpClient.GetStream();

			// Send the message to the connected TcpServer. 
			stream.Write(data, 0, data.Length);
		}

		void IDisposable.Dispose()
		{
			//_tcpClient.GetStream().Close();
			//_tcpClient.Close();
		}
	}
}

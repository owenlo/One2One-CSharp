using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace One2One.Lib.Server
{
	public delegate void MessageReceivedHandler(object sender, MessageReceivedEventArgs e); //delegate is defined outside our class, inside the namespace, visible to all classes.

	public class TcpServer : ITcpServer
	{
		public event MessageReceivedHandler MessageReceived;

		private IPAddress _address;
		private int _port;

		private ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

		public TcpServer(string address, int port)
		{
			_address = IPAddress.Parse(address);
			_port = port;
		}

		void ITcpServer.Listen()
		{
			TcpListener tcpListener = new TcpListener(_address, _port);	
			tcpListener.Start();

			tcpListener.BeginAcceptTcpClient(this.OnAcceptConnection, tcpListener);		

			tcpClientConnected.WaitOne();
		}

		private void OnAcceptConnection(IAsyncResult asyn)
		{
			// Get the listener that handles the client request.
			TcpListener listener = (TcpListener)asyn.AsyncState;

			// Get the newly connected TcpClient
			TcpClient client = listener.EndAcceptTcpClient(asyn);

			Byte[] bytes = new Byte[256];
			String data = null;

			try
			{
			// Get a stream object for reading and writing
			NetworkStream stream = client.GetStream();

			int i;
			// Loop to receive all the data sent by the client. 			
			while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
			{
				// Translate data bytes to a ASCII string.
				data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
				MessageReceivedEventArgs e = new MessageReceivedEventArgs(data);
				OnMessageReceived(e);
			}
			}
			catch (Exception){}
						
			tcpClientConnected.Set();
		}
		
		protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
		{
			if (MessageReceived != null)
			{
				MessageReceived(this, e);
			}
		}

		void IDisposable.Dispose()
		{
		
		}
	}
}

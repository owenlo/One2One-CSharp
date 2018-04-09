using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One2One.Lib.Server
{
	public class MessageReceivedEventArgs
	{
		private string _message;
		public MessageReceivedEventArgs(string message)
		{
			this._message = message;
		}

		public string MessageReceived
		{
			get
			{
				return _message;
			}
		}
	}
}

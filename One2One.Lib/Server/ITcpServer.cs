using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One2One.Lib.Server
{			
	public interface ITcpServer : IDisposable
	{
		event MessageReceivedHandler MessageReceived;
		void Listen();		
	}
}

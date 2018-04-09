using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One2One.Lib.Client
{
	public interface ITcpClient : IDisposable
	{
		void Connect();
		void Message(string message);
	}
}

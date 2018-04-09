using One2One.Lib.Client;
using One2One.Lib.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One2One.App
{
	public class NetworkModel
	{
		public ITcpServer Server { get; set; }
		public ITcpClient Client { get; set; }

		public string LocalAddress { get; set; }
		public int LocalPort { get; set; }

		public string ExernalAddress { get; set; }
		public int ExernalPort { get; set; }
	}
}

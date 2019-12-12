using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFT_Launcher_12
{
	public class ServerConfig
	{
		public ServerData server;
		public DebugData debug;

		public class ServerData
		{
			public string backendUrl;
			public string ip;
			public int port;
			public bool generateIp;
		}

		public class DebugData
		{
			public bool ExaminedByDefault;
			public bool loadingDisplayer;
			public bool showSeparator;
			public bool debugMode;
			public bool disableLogsDisplayer;
		}
	}
}

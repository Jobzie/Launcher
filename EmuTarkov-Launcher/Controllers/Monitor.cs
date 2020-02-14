using System;
using System.Diagnostics;
using System.Timers;

namespace EmuTarkov_Launcher
{
	public class Monitor
	{
		private System.Timers.Timer monitor;
		private string processName;
		private Action<Monitor> callback;

		public Monitor(string processName, Action<Monitor> callback)
		{
			monitor = new System.Timers.Timer(1000);
			monitor.Elapsed += OnPollEvent;
			monitor.AutoReset = true;

			this.processName = processName;
			this.callback = callback;
		}

		public void Start()
		{
			monitor.Enabled = true;
		}

		public void Stop()
		{
			monitor.Enabled = false;
		}

		private void OnPollEvent(Object source, ElapsedEventArgs e)
		{
			Process[] clientProcess = Process.GetProcessesByName(processName);

			// client instances still running
			if (clientProcess.Length > 0)
			{
				return;
			}

			// all client instances stopped running
			callback(this);
		}
	}
}

using System.Diagnostics;
using System.IO;
using Launcher.Code.Monitor;

namespace Launcher.Code.Starter
{
    public class StarterBase
    {
        private string filepath = "";
        private string executable = "";
        private string arguments = "";

        protected StarterBase(string filepath, string executable, string arguments = "")
        {
            this.filepath = filepath;
            this.executable = executable;
            this.arguments = arguments;
        }

        protected void Start()
        {
            ProcessStartInfo process = new ProcessStartInfo();
            process.FileName = Path.Combine(filepath, executable);
            process.UseShellExecute = false;
            process.WorkingDirectory = filepath;
            if(arguments != "")
                process.Arguments = arguments;

            if (File.Exists(process.FileName))
            {
                Process gameProcess = Process.Start(process);
            }
        }
    }
}

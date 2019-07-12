namespace Launcher.Code.Starter
{
    class ServerStarter : StarterBase
    {
        public ServerStarter(string filepath) : base(filepath, "EmuTarkov-Server.exe")
        {
            base.Start();
        }
    }
}

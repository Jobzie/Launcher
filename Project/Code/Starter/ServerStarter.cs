namespace Launcher.Code.Starter
{
    class ServerStarter : StarterBase
    {
        public ServerStarter(string filepath, string filename) : base(filepath, filename + ".exe")
        {
            base.Start();
        }
    }
}

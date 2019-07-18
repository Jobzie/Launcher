namespace Launcher.Code.Data
{
    public class Errors
    {
        public int code = 0;
        public string text = "";
        public Errors(int c = 0, string t = "") {
            this.code = c;
            this.text = t;
        }
    }

    public class ErrorTypes
    {
        public Errors error_Client_noLocation   = new Errors(101, "Unable to find proper files; Check Client location in settings.");
        public Errors error_Server_noLocation   = new Errors(102, "Unable to find proper files; Check Server location in settings.");
        public Errors error_noUserLikeThis      = new Errors(44, "User not found.");
        public Errors error_ClientAlreadyRunning = new Errors(201, "Client is already running.");
        public Errors error_ServerAlreadyRunning = new Errors(202, "Server is already running.");
    }
}

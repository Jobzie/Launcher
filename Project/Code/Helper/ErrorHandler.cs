using Launcher.Code.Data;
using Launcher.Code.Settings;
using System.Collections.Generic;
using System.IO;

namespace Launcher.Code.Helper
{
    public partial class ErrorHandler
    {
        private List<Errors> storeErrors = new List<Errors>(); // holds up to 10 errors

        public ErrorHandler()
        {
            // just to make handle
        }
        public bool isAnyErrors() {
            if (storeErrors.Count > 0)
                return true;
            else
                return false;
        }
        public int StringErrLines(string s) {
            string[] arrString = s.Split("\n".ToCharArray());
            int lines = arrString.Length;
            for (int i = 0; i < arrString.Length; i++) {
                if (arrString[i].Length > 62)
                    lines++;
            }
            // calculated lines
            // [lines * (fontSize + 2)] = base height of the textbox
            // [aboveCalc + 20] = base height of the error box
            lines--;
            return lines;
        }
        public string ReturnErrorAsText() {
            string endString = "";
            foreach (Errors err in storeErrors)
            {
                endString = endString + "- [ " + err.code + " ] " + err.text + "\n";
            }
            return endString;
        }

        public void AddError(int code, string text) {
            foreach (Errors e in storeErrors) {
                if(e.code == code)
                    return;// do not add it multiply times
            }
            Errors error = new Errors();
            error.code = code;
            error.text = text;
            storeErrors.Add(error);
        }

        public void RemoveError(int code, string text) {
            foreach (Errors e in storeErrors)
            {
                if (e.code == code)
                {
                    storeErrors.Remove(e);
                    return;
                }
            }
        }


       /* public void ErrorCheck() {
           foreach(Errors e in storeErrors)
           {
                serverSettings = new ServerSettings(System.IO.Path.Combine(laucherSettings.GetServerLocation(), "data"));
                laucherSettings = new LauncherSettings();
                ProfileSettings = new ProfileSettings(Path.Combine(laucherSettings.GetServerLocation(), "data/profiles"));

                switch (e.code) {
                    case 100: // is profile loaded properly
                       //we not remove login failed messages
                        break;
                    case 101:
                        if (ProfileSettings.ListExists())
                            storeErrors.Remove(e);
                        break;
                    case 111:
                        string check_client_file = laucherSettings.GetClientLocation() + @"\" + laucherSettings.GetClientFilename() + ".exe";
                        if (File.Exists(check_client_file))
                            storeErrors.Remove(e);
                        break;
                    case 112:
                        string check_server_file = laucherSettings.GetServerLocation() + @"\" + laucherSettings.GetServerFilename() + ".exe";
                        if (File.Exists(check_server_file))
                            storeErrors.Remove(e);
                        break;
                    case 103:
                        break;
                    case 104:
                        break;

                }


            }



        }*/


    }
}

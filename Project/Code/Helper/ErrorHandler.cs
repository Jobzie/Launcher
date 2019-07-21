using Launcher.Code.Data;
using Launcher.Code.Settings;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace Launcher.Code.Helper
{
    public partial class ErrorHandler
    {
        private List<Errors> storeErrors = new List<Errors>(); // holds up to 10 errors

        public ErrorHandler()
        {
            // just to make handle
        }
        
        // check if any errors remaining (bool)
        public bool isAnyErrors() {
            if (storeErrors.Count > 0)
                return true;
            else
                return false;
        }

        public bool isError(int code) {
            foreach (Errors e in storeErrors)
            {
                if (e.code == code)
                    return true;// do not add it multiply times
            }
            return false;
        }
        // determine how big the errors string is /width and height/
        // calculated lines
        // [lines * (fontSize + 2)] = base height of the textbox
        // [aboveCalc + 20] = base height of the error box
        public int StringErrLines(string s) {
            string[] arrString = s.Split("\n".ToCharArray());
            int lines = arrString.Length;
            for (int i = 0; i < arrString.Length; i++) {
                if (arrString[i].Length > 62)
                    lines++;
            }
            lines--;
            return lines;
        }
        // return errors as Prepared String with newline indicators "\n"
        public string ReturnErrorAsText() {
            string endString = "";
            foreach (Errors err in storeErrors)
            {
                endString = endString + "- [ " + err.code + " ] " + err.text + "\n";
            }
            return endString;
        }
        // add new error if happend /without multiply added errors
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
        public void AddError(Errors err) {
            foreach (Errors e in storeErrors)
            {
                if (e.code == err.code)
                    return;// do not add it multiply times
            }
            storeErrors.Add(err);
        }
        // remove error if not happening /remove it by error code
        public void RemoveError(int code) {
            foreach (Errors e in storeErrors)
            {
                if (e.code == code)
                {
                    storeErrors.Remove(e);
                    return;
                }
            }
        }
        // yea i know its primitive solution ...
        public void RemoveError(Errors err) {
            foreach (Errors e in storeErrors)
            {
                if (e.code == err.code)
                {
                    storeErrors.Remove(e);
                    return;
                }
            }
        }
        //prevent from starting server if destinition is unknown
        public void ButtonDisplayer(Button btn_Start_Client, Button btn_Start_Server) {
            bool err_101=false, err_102=false;
            /*
             101 = client error
             102 - server error
             */
            foreach (Errors err in storeErrors)
            {
                switch (err.code) {
                    case 101:
                        err_101 = true;
                        btn_Start_Client.Visibility = System.Windows.Visibility.Hidden;

                        break;
                    case 102:
                        err_102 = true;
                        btn_Start_Server.Visibility = System.Windows.Visibility.Hidden;

                        break;
                    default:
                        btn_Start_Client.Visibility = System.Windows.Visibility.Visible;
                        btn_Start_Server.Visibility = System.Windows.Visibility.Visible;
                        break;
                }
            }
                if (!err_101)
                    btn_Start_Client.Visibility = System.Windows.Visibility.Visible;
                if (!err_102)
                    btn_Start_Server.Visibility = System.Windows.Visibility.Visible;

        }
    }
}

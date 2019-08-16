using System;

using System.Windows;
using System.IO;
using System.Text;
using Launcher;


namespace Launcher.Code.Helper
{
    public class Tools
    {
        public void SaveFileStream(Uri uri, String path)
        {
            var resourceStream = Application.GetResourceStream(uri).Stream;
            using (StreamReader reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                reader.BaseStream.CopyTo(fileStream);
                fileStream.Dispose();
            }
        }
        public bool ExistF(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }
        public bool ExistD(string path)
        {
            if (Directory.Exists(path))
                return true;
            return false;
        }
    }
}

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
    }
}

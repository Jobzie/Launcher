using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace EmuTarkov_Launcher
{
	public static class LauncherRequest
	{
		public static string Send(string url, string data)
		{
			//try
			//{
				SSLValidator.OverrideValidation();
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
				byte[] requestData = Encoding.UTF8.GetBytes(data);

				// set header
				request.Method = "POST";
				request.ContentType = "application/json";
				request.Headers[HttpRequestHeader.ContentEncoding] = "deflate";
				request.ContentLength = requestData.Length;

				// set data
				using (Stream stream = request.GetRequestStream())
				{
					using (DeflateStream zip = new DeflateStream(stream, CompressionMode.Compress))
					{
						zip.Write(requestData, 0, requestData.Length);
					}
				}

				// get response
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				using (Stream stream = response.GetResponseStream())
				{
					using (DeflateStream zip = new DeflateStream(stream, CompressionMode.Decompress))
					{
						using (StreamReader sr = new StreamReader(zip))
						{
							return sr.ReadToEnd();
						}
					}
				}
			//}
			//catch
			//{
			//	return null;
			//}
		}
	}
}
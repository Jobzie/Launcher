﻿using System.IO;
using System.Net;
using System.Text;

namespace EmuTarkov_Launcher
{
	public static class LauncherRequest
	{
		public static string Send(string url, string data)
		{
			try
			{
				SSLValidator.OverrideValidation();
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new System.Uri(url));
				byte[] requestData = Zip.Compress(Encoding.UTF8.GetBytes(data));

				// set header
				request.Method = "POST";
				request.ContentType = "application/json";
				request.ContentLength = requestData.Length;

				// set data
				using (Stream stream = request.GetRequestStream())
				{
					stream.Write(requestData, 0, requestData.Length);
				}

				// get response
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				using (Stream stream = response.GetResponseStream())
				{
					using (MemoryStream ms = new MemoryStream())
					{
						stream.CopyTo(ms);
						return Encoding.UTF8.GetString(Zip.Decompress(ms.ToArray()));
					}
				}
			}
			catch
			{
				return null;
			}
		}
	}
}
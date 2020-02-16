using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace EmuTarkov_Launcher
{
	public static class SSLValidator
	{
		private static bool OnValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public static void OverrideValidation()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
			ServicePointManager.ServerCertificateValidationCallback = OnValidateCertificate;
		}
	}
}

using System.IO;
using System.IO.Compression;

namespace EmuTarkov_Launcher
{
	public static class Zip
	{
		public static byte[] Compress(byte[] data)
		{
			using (MemoryStream output = new MemoryStream())
			{
				using (DeflateStream zip = new DeflateStream(output, CompressionMode.Compress))
				{
					zip.Write(data, 0, data.Length);
					return output.ToArray();
				}
			}
		}

		public static byte[] Decompress(byte[] data)
		{
			using (MemoryStream input = new MemoryStream(data))
			{
				using (MemoryStream output = new MemoryStream())
				{
					using (DeflateStream zip = new DeflateStream(input, CompressionMode.Decompress))
					{
						zip.CopyTo(output);
						return output.ToArray();
					}
				}
			}
		}
	}
}

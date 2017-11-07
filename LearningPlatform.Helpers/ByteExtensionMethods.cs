using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.Helpers
{
	public static class ByteExtensionMethods
	{
		public static string ByteArrayToString(this byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);
			foreach (byte b in ba)
				hex.AppendFormat("{0:x2}", b);
			return hex.ToString();
		}

		public static Stream ToStream(this byte[] byteArray)
		{
			Stream stream = null;
			if (byteArray != null && byteArray.Length > 0)
			{
				stream = new MemoryStream(byteArray);
			}
			else
			{
				stream = CreateEmptyImage();
			}

			return stream;
		}

		private static Stream CreateEmptyImage()
		{
			MemoryStream stream = new MemoryStream();
			using (var image = new Bitmap(215, 40))
			{
				image.MakeTransparent();
				image.Save(stream, ImageFormat.Png);
			}

			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}
	}
}

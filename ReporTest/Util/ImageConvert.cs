using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReporTest.Util
{
    public static class ImageConvert
    {
        public static string ConvertToBase64(Stream imageStream)
        {
            byte[] imageArray = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                imageArray = memoryStream.ToArray();
            }
            string base64Image = Convert.ToBase64String(imageArray);
            return base64Image;
        }
    }
}

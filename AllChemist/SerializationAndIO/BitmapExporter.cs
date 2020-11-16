using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace AllChemist.SerializationAndIO
{
    public static class BitmapExporter
    {
        public static string folderName = "Exports";
        public static void Export(WriteableBitmap bitmap, BitmapEncoder encoder)
        {
            string dest = AppDomain.CurrentDomain.BaseDirectory + "\\" + folderName + "\\" + ExportFilename() + ".png";
            using (FileStream stream =
                new FileStream(dest, FileMode.Create))
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
            }
            Console.WriteLine("Exported as: "+dest);
        }
        
        private static string ExportFilename()
        {
            return "Allchemist_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
        }
    }

}

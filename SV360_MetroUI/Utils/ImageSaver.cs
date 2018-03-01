using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Drawing;

namespace SV360.Utils
{

    [Obsolete("Plus utilisé")]
    public class ImageSaver 
    {
        //Folder path
        private static string path;
        private static Bitmap image;

        public ImageSaver(string Path, Bitmap img)
        {
            path = Path;
            image = (Bitmap)img.Clone();
            SaveFile();
        }

        public void SaveFile()
        {
            //Save image file via another task
            Thread threadSaveFile = new Thread(new ThreadStart(SaveFileTask));
            threadSaveFile.Start();
        }

        private void SaveFileTask()
        {      
            //save it as a file in the coressponding folder
            image.Save(path, System.Drawing.Imaging.ImageFormat.Bmp);
        }
    }
}

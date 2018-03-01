using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;
using SV360.Data;
using Emgu.CV.UI;

namespace SV360.Preprocessing
{
    /// <summary>
    /// Pre Processing for Global Vision, when seeds fall in the TCCAGE
    /// </summary>
    public class GlobalVisionPP : Preprocessing
    {

        /// <summary>
        /// Background Image
        /// </summary>
        public Image<Rgb, Byte> imBg;

        private int XCrop = 450, HeightCrop = 600;

        public override bool preprocessing(Acquisition acquisition)
        {
            Image<Rgb, Byte> imSrc = acquisition.Image;

            #region manual cropping
            imSrc.ROI = new Rectangle(0, XCrop, imSrc.Width, HeightCrop);
            imBg.ROI = new Rectangle(0, XCrop, imSrc.Width, HeightCrop);
            #endregion

            Image<Gray, Byte> threshIm = Segmentation(imSrc);

            #region Finding Contours
            Contour<Point> contour;
            MemStorage storage = new MemStorage();//allocate storage for contour approximation
            for (contour = threshIm.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL, storage); contour != null; contour = contour.HNext)
            {
                if (contour.Area > 730) //only consider contours with area greater than 200
                {
                    View viewTemp = new View();
                    viewTemp.Contour = contour;
                    viewTemp.Roi = CvInvoke.cvBoundingRect(contour, false);
                    viewTemp.ApproxContour = contour.ApproxPoly((contour.Perimeter / 10), storage);

                    viewTemp.Image = imSrc.GetSubRect(viewTemp.Roi);
                    viewTemp.Image = viewTemp.Image.Copy(threshIm.GetSubRect(viewTemp.Roi));

                    acquisition.AddView(viewTemp);
                }
            }

            if (acquisition.Views.Count < 4)
            {
                Debug.WriteLine(" Less than 4 Contours found");
                return false;
            }
            #endregion

            #region sorting views with 4 bigger area and left to right order

            acquisition.Views.Sort((x, y) => (y.Roi.Size.Height * y.Roi.Size.Width).CompareTo(x.Roi.Size.Height * x.Roi.Size.Width)); // bigger

            if (acquisition.Views.Count > 4)
                acquisition.Views.RemoveRange(4, acquisition.Views.Count - 4);

            acquisition.Views.Sort((x, y) => x.Roi.X.CompareTo(y.Roi.X));  //left to right

            for (int i = 0; i < acquisition.Views.Count; i++)
                acquisition.Views[i].Id = i;

            /* for (int i = 0; i < views.Count; i++)
                 Debug.WriteLine("1st sorting:" + i + " : " + views[i].Roi.Size.Height * views[i].Roi.Size.Width + "->" + views[i].Roi.X );
             */
            #endregion

            /*  for (int i = 0; i < acquisition.Views.Count; i++)
                  acquisition.Views[i].Image.Save(@"D:\Users\damien.delhay\Documents\Travail\Images\test_SDKJAI\Preproc\test" + i + ".bmp");
            */
            for (int i = 0; i < acquisition.Views.Count; i++)
                acquisition.Views[i].Image.Save(@"C:\Users\mat.tsai1\Desktop\farouk-Images\test" + i + ".bmp");

            return true;
        }

        private Image<Gray, Byte> Segmentation(Image<Rgb, Byte> imSrc)
        {
            Image<Gray, Byte> threshIm = new Image<Gray, byte>((imSrc).AbsDiff(imBg).Convert<Gray, Byte>().Data);
            Image<Gray, Byte> border = new Image<Gray, byte>(threshIm.Width + 80, threshIm.Height + 80);

            CvInvoke.cvCopyMakeBorder(threshIm, border, new Point(40, 40), Emgu.CV.CvEnum.BORDER_TYPE.CONSTANT, new MCvScalar(0));
            threshIm = border;

            threshIm = threshIm.ThresholdToZero(new Gray(30));

            // ImageViewer.Show(threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));

            //CvInvoke.cvThreshold(threshIm, threshIm, 30, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY | Emgu.CV.CvEnum.THRESH.CV_THRESH_OTSU);

            threshIm = threshIm.ThresholdBinary(new Gray(30), new Gray(255));



            // ImageViewer.Show(threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));
            //CvInvoke.cvShowImage("otsu sub", threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));

            StructuringElementEx elementOpen = new StructuringElementEx(3, 3, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
            threshIm = threshIm.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_OPEN, 7);
            //CvInvoke.cvShowImage("open sub", threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));

            int size = 2;
            elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
            threshIm = threshIm.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, 1);
            //CvInvoke.cvShowImage("close 1 sub", threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));

            size = 3;
            elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
            threshIm = threshIm.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_OPEN, 3);
            //CvInvoke.cvShowImage("open 2 sub", threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));


            size = 3;
            elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
            threshIm = threshIm.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, 9);


            threshIm.ROI = new Rectangle(40, 40, threshIm.Width - 80, threshIm.Width - 80);

            // CvInvoke.cvShowImage("Segmentation result", threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));

            return threshIm;
        }
    }
}

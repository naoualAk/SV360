using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using SV360.Data;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using Emgu.CV.UI;

namespace SV360.Preprocessing
{
    /// <summary>
    /// Preprocessing dédié au convoyeur à bande bleu
    /// </summary>
    public class ConvoyeurPP : Preprocessing
    {

        /// <summary>
        /// image background
        /// </summary>
        private Image<Rgb, Byte> imBg;
        /// <summary>
        /// nombre d'images pour faire le background
        /// </summary>
        public int nbBg = 0;
        private int contourMax = 2;


        /// <summary>
        /// Image background
        /// </summary>
        public Image<Rgb, byte> ImBg
        {
            get
            {
                return imBg;
            }
        }


        /// <summary>
        /// Ajoute une image à l'image background en faisant une moyenne
        /// </summary>
        /// <param name="bg"></param>
        public void AddBg(Image<Rgb, byte> bg)
        {
            nbBg++;
            Debug.WriteLine(nbBg);
            if (nbBg == 1)
            {
                imBg = bg;
            }
            else
            {
                double coef = 1 / (double)(nbBg);
                imBg = imBg.AddWeighted(bg, 1 - coef, coef, 0);
            }
        }


        /// <summary>
        /// Réalise le preprocessing (segmentation, cropping) d'une acquisition
        /// </summary>
        /// <param name="acquisition">acquisition</param>
        /// <returns></returns>
        public override bool preprocessing(Acquisition acquisition)
        {
            Image<Rgb, Byte> imSrc = acquisition.Image.Copy();

            Rectangle[] rois = new Rectangle[2];

            if (imSrc[0, 0].Equals(Color.Black) && imSrc[8, 16].Equals(Color.Black))
            {


                return false;
            }

            /*#region test 14-09
           //gauche
            int xRoi = 800 / 2, yRoi = 100 / 2;
            int xRBRoi = 2400 / 2, yRBRoi = 3100 / 2;
            rois[0] = new Rectangle(xRoi, yRoi, xRBRoi - xRoi, yRBRoi - yRoi);
            //droite
            xRoi = 2840 / 2; yRoi = 100 / 2;
            xRBRoi = 4860 / 2; yRBRoi = 3100 / 2;
            rois[1] = new Rectangle(xRoi, yRoi, xRBRoi - xRoi, yRBRoi - yRoi);
            #endregion*/

            /*#region 23-11 position haute
            //gauche
            int xRoi = 0, yRoi = 100 / 2;
            int xRBRoi = 1000, yRBRoi = 2200;
            rois[0] = new Rectangle(xRoi, yRoi, xRBRoi - xRoi, yRBRoi - yRoi);
            //droite
            xRoi = 1200; yRoi = 100 / 2;
            xRBRoi = 2300; yRBRoi = 2200;
            rois[1] = new Rectangle(xRoi, yRoi, xRBRoi - xRoi, yRBRoi - yRoi);
            #endregion*/

            //Decoupage en Region convoyeur et miroir
            #region ROI SV360 03-02
            //gauche
            int xRoi = 70, yRoi = 10;
            int xRBRoi = 1510, yRBRoi = 2410;
            rois[0] = new Rectangle(xRoi, yRoi, xRBRoi - xRoi, yRBRoi - yRoi);
            //droite
            xRoi = 1930; yRoi = 10;
            xRBRoi = 3010; yRBRoi = 2410;
            rois[1] = new Rectangle(xRoi, yRoi, xRBRoi - xRoi, yRBRoi - yRoi);
            #endregion


            //Convertion to Grayscale
            // Image<Gray, Byte> threshIm = ObsoleteSegmentation(imSrc, rois);

            Image<Gray, Byte> threshIm = Segmentation(imSrc, imBg, rois[1], rois[0]);
            //CvInvoke.cvShowImage("Final Segmentation", imSrc.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).Convert<Bgr, Byte>());
            imSrc.ROI = threshIm.ROI;
            imBg.ROI = threshIm.ROI;

            Contour<Point> contour;




            #region Finding Contours
            MemStorage storage = new MemStorage();//allocate storage for contour approximation
            for (contour = threshIm.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_EXTERNAL, storage); contour != null; contour = contour.HNext)
            {
                if (contour.Area > 4900) //only consider contours with area greater than  1mmx1mm = 70 x 70  = 4900
                {
                    View viewTemp = new View();
                    viewTemp.Roi = CvInvoke.cvBoundingRect(contour, false);
                    if (HasRectInImage(viewTemp.Roi, threshIm.Size)) // only consider the contour inside the image
                    {
                        viewTemp.Contour = contour;
                        viewTemp.ConvexHull = contour.GetConvexHull(Emgu.CV.CvEnum.ORIENTATION.CV_COUNTER_CLOCKWISE, storage);
                        viewTemp.ApproxContour = contour.ApproxPoly((contour.Perimeter / 600), storage);
                        viewTemp.Image = imSrc.GetSubRect(viewTemp.Roi);
                        viewTemp.Image = viewTemp.Image.Copy(threshIm.GetSubRect(viewTemp.Roi));


                        //if (!IsDoubleSeeds(threshIm.GetSubRect(viewTemp.Roi)))
                        acquisition.AddView(viewTemp);
                    }
                }
            }
            #endregion

            #region sorting views with 2 bigger area and left to right order
            acquisition.Views.Sort((x, y) => ((double)y.Contour.Area).CompareTo((double)x.Contour.Area)); // bigger

            if (acquisition.Views.Count > contourMax)
                acquisition.Views.RemoveRange(contourMax, acquisition.Views.Count - contourMax);

            if (acquisition.Views.Count < contourMax)
            {
                Debug.WriteLine(" Less than {0} contours found", contourMax);
                return false;
            }

            acquisition.Views.Sort((x, y) => x.Roi.X.CompareTo(y.Roi.X));  //left to right

            for (int i = 0; i < acquisition.Views.Count; i++)
                acquisition.Views[i].Id = i;
            #endregion


            // Check if the seed is not in extremity (right or left) of belt
            if (acquisition.Views[0].Roi.Right >= rois[0].Right - 5)
            {
                Debug.WriteLine(" The seed is not on the middle of belt", contourMax);
                return false;
            }

            // Check if the two views are in the same horizontal position
            /*  if ()
              {
                  Debug.WriteLine(" The seed is not on the middle of belt", contourMax);
                  return false;
              }*/

            #region Drawing

            Image<Gray, Byte> mask = new Image<Gray, byte>(threshIm.Width, threshIm.Height, new Gray(0));
            for (int i = 0; i < acquisition.Views.Count; i++)
            {
                mask.FillConvexPoly(acquisition.Views[i].ApproxContour.ToArray(), new Gray(1));
            }

            /* int size = 10;
             StructuringElementEx elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
             mask = mask.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_OPEN, 2);
             */
            //ImageViewer.Show(mask);
            //acquisition.Image = imSrc.Copy(mask).Add(mask.Mul(255).Not().Convert<Rgb, byte>());
            acquisition.Image = imSrc.Copy();
            for (int i = 0; i < acquisition.Views.Count; i++)
                acquisition.Image.Draw(acquisition.Views[i].ApproxContour, new Rgb(255, 199, 44), 7);

            //.Add(mask.Not().Convert<Rgb, byte>().Mul(255));

            //ImageViewer.Show(acquisition.Image);
            // Mul(mask.Convert<Rgb, byte>()).Add(mask.Not().Convert<Rgb, byte>().Mul(255));

            //Image<Gray, Byte> mask = threshIm.Copy();


            /*  Image<Rgb, float> maskF = mask.Convert<Rgb, float>();
              maskF = maskF.Mul((float)1 / 255);
              Image<Rgb, float> imageF = imSrc.Convert<Rgb, float>();
              maskF.ROI = new Rectangle(0, 0, maskF.Width, maskF.Height);
              imageF = imageF.Mul(maskF);
              acquisition.Image = imageF.Mul(maskF).Convert<Rgb, Byte>().Add(mask.Not().Convert<Rgb,byte>()) ;*/

            #endregion

            return true;
        }

        private bool IsDoubleSeeds(Image<Gray, byte> thresholdImage)
        {

            return false;
        }

        /*private  bool ApproxEquals(this int value1, int value2, float pourcent)
         {

             if (value1 <= value2 * (1+pourcent) && value1 >= value2 * (1 - pourcent))
                 return true;
             else return false;
         }

         private bool ApproxEquals(this int value1, int value2, int compareValue)
         {
             if (value1 <= value2 + compareValue && value1 >= value2 - compareValue)
                 return true;
             else return false;
         }*/

        /// <summary>
        /// Segmentation miroir et bande
        /// </summary>
        /// <param name="imSrc"></param>
        /// <param name="imBg"></param>
        /// <param name="roiMirror"></param>
        /// <param name="roiBelt"></param>
        /// <returns></returns>
        private Image<Gray, Byte> Segmentation(Image<Rgb, Byte> imSrc, Image<Rgb, Byte> imBg, Rectangle roiMirror, Rectangle roiBelt)
        {
            int maxUp = imSrc.Height, maxRight = 0, maxLeft = imSrc.Width, maxBottom = 0;

            int width = imSrc.Width, height = imSrc.Height;

            Image<Gray, Byte> threshTot = new Image<Gray, Byte>(width, height);


            maxLeft = roiBelt.X < roiMirror.X ? roiBelt.X : roiMirror.X;
            maxUp = roiBelt.Y < roiMirror.Y ? roiBelt.Y : roiMirror.Y;
            maxRight = roiBelt.Right > roiMirror.Right ? roiBelt.Right : roiMirror.Right;
            maxBottom = roiBelt.Bottom > roiMirror.Bottom ? roiBelt.Bottom : roiMirror.Bottom;

            Thread mirrorThread;

            Image<Gray, Byte> resMirror = null;
            Image<Rgb, Byte> tp1 = imSrc.Copy(roiMirror), tp2 = imBg.Copy(roiMirror);
            mirrorThread = new Thread(() => { resMirror = SegMirror(tp1, tp2); });
            mirrorThread.Start();
            // resMirror = SegMirror(imSrc.Copy(roiMirror), imBg.Copy(roiMirror)); // sequential part

            Image<Gray, Byte> resBelt = SegBelt(imSrc.Copy(roiBelt), imBg.Copy(roiBelt));

            mirrorThread.Join();

            threshTot.ROI = new Rectangle(roiMirror.X, roiMirror.Y, roiMirror.Width, roiMirror.Height);
            resMirror.CopyTo(threshTot);
            threshTot.ROI = new Rectangle(roiBelt.X, roiBelt.Y, roiBelt.Width, roiBelt.Height);
            resBelt.CopyTo(threshTot);

            threshTot.ROI = new Rectangle(maxLeft, maxUp, maxRight - maxLeft, maxBottom - maxUp);

            return threshTot;
        }

        /// <summary>
        /// segmentation sur bande
        /// </summary>
        /// <param name="imSrc"></param>
        /// <param name="imBg"></param>
        /// <returns></returns>
        private Image<Gray, Byte> SegBelt(Image<Rgb, Byte> imSrc, Image<Rgb, Byte> imBg)
        {
            //imSrc.ROI = roi;
            //imBg.ROI = roi;

            Image<Lab, Byte> srcLab = imSrc.Convert<Lab, Byte>();
            Image<Lab, Byte> bgLab = imBg.Convert<Lab, Byte>();

            Image<Gray, Byte> ImL = new Image<Gray, Byte>(srcLab.Split()[0].AbsDiff(bgLab.Split()[0]).Data);
            //ImL._ThresholdToZero(new Gray(55));

            Image<Gray, Byte> ImB = new Image<Gray, Byte>(srcLab.Split()[2].AbsDiff(bgLab.Split()[2]).Data);
            ImB = ImB.Add(ImL.Mul(0.3));

            Image<Gray, Byte> border = new Image<Gray, Byte>(ImB.Width + 80, ImB.Height + 80);

            CvInvoke.cvCopyMakeBorder(ImB, border, new Point(40, 40), Emgu.CV.CvEnum.BORDER_TYPE.CONSTANT, new MCvScalar(0));
            ImB = border;
            CvInvoke.cvThreshold(ImB, ImB, 40, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);
            //CvInvoke.cvThreshold(ImB, ImB, 0, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY | Emgu.CV.CvEnum.THRESH.CV_THRESH_OTSU);

            int size = 2;
            StructuringElementEx elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
            ImB = ImB.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_OPEN, 2);

            // ouverture 
            size = 3;
            elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            ImB = ImB.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, 2);

            ImB.ROI = new Rectangle(40, 40, ImB.Width - 80, ImB.Height - 80);

            return ImB;
        }




        /// <summary>
        /// segmentation sur miroir
        /// </summary>
        /// <param name="imSrc"></param>
        /// <param name="imBg"></param>
        /// <returns></returns>
        private Image<Gray, Byte> SegMirror(Image<Rgb, Byte> imSrc, Image<Rgb, Byte> imBg)
        {
            //imSrc.ROI = roi;
            //imBg.ROI = roi;

            Image<Lab, Byte> srcLab = imSrc.Convert<Lab, Byte>();
            Image<Lab, Byte> bgLab = imBg.Convert<Lab, Byte>();

            Image<Gray, Byte> ImL = new Image<Gray, Byte>(srcLab.Split()[0].AbsDiff(bgLab.Split()[0]).Data);
            //ImL._ThresholdToZero(new Gray(120));

            Image<Gray, Byte> ImB = new Image<Gray, Byte>(srcLab.Split()[2].AbsDiff(bgLab.Split()[2]).Data);
            ImB = ImB.Add(ImL.Mul(0.4));

            Image<Gray, Byte> border = new Image<Gray, Byte>(ImB.Width + 80, ImB.Height + 80);

            CvInvoke.cvCopyMakeBorder(ImB, border, new Point(40, 40), Emgu.CV.CvEnum.BORDER_TYPE.CONSTANT, new MCvScalar(0));
            ImB = border;
            CvInvoke.cvThreshold(ImB, ImB, 40, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);

            int size = 2;
            StructuringElementEx elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
            ImB = ImB.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_OPEN, 1);


            // ouverture 
            size = 3;
            elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT);
            ImB = ImB.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, 2);

            ImB.ROI = new Rectangle(40, 40, ImB.Width - 80, ImB.Height - 80);
            // CvInvoke.cvShowImage("ImB MorphologyEx", ImB.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));
            return ImB;
        }

        /// <summary>
        /// Vérifie si le rectangle est à l'intérieur d'une fenetre (size)
        /// </summary>
        /// <param name="rInside">rectangle</param>
        /// <param name="size">fenetre</param>
        /// <returns></returns>
        private bool HasRectInImage(Rectangle rInside, Size size)
        {
            if (rInside.Top <= 5 || rInside.Right <= 5 || rInside.Bottom >= size.Height - 5 || rInside.Left >= size.Width - 5)
                return false;
            else return true;
        }


        [Obsolete("plus utilisé")]
        public Image<Gray, Byte> ObsoleteSegmentation(Image<Rgb, Byte> imSrc, Rectangle[] rois)
        {

            int maxUp = imSrc.Height, maxRight = 0, maxLeft = imSrc.Width, maxBottom = 0;
            if (rois.Length > 0)
            {
                int width = imSrc.Width, height = imSrc.Height;

                Image<Gray, Byte> threshTot = new Image<Gray, Byte>(width, height);

                for (int i = 0; i < rois.Length; i++)
                {
                    string strIdx = i.ToString();
                    if (maxLeft > rois[i].X) maxLeft = rois[i].X;
                    if (maxUp > rois[i].Y) maxUp = rois[i].Y;
                    if (maxRight < rois[i].Right) maxRight = rois[i].Right;
                    if (maxBottom < rois[i].Bottom) maxBottom = rois[i].Bottom;

                    imSrc.ROI = rois[i];
                    imBg.ROI = rois[i];


                    Image<Lab, Byte> srcLab = imSrc.Convert<Lab, Byte>();
                    Image<Lab, Byte> bgLab = imBg.Convert<Lab, Byte>();

                    ///mask for the shadow
                    //  Image<Gray, Byte> shadowMask = new Image<Gray, Byte>(srcLab.Split()[0].Data);
                    // shadowMask=shadowMask.ThresholdBinary(new Gray(15), new Gray(1));

                    /// |src - bg| && shadowMask
                    //Image<Gray, Byte>  testC = new Image<Gray, Byte>(srcLab.Split()[2].AbsDiff(bgLab.Split()[2]).Mul(shadowMask).Data);
                    // CvInvoke.cvShowImage("testV" + strIdx, testC.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));

                    /// a of LAB : | src - bg | x shadowMask
                    //   Image<Gray, Byte> blurMask = new Image<Gray, Byte>(srcLab.Split()[1].AbsDiff(bgLab.Split()[1]).Data);
                    //   CvInvoke.cvShowImage("a" + strIdx, blurMask.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));
                    //  CvInvoke.cvThreshold(blurMask, blurMask, 0,1, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY | Emgu.CV.CvEnum.THRESH.CV_THRESH_OTSU);
                    // CvInvoke.cvShowImage("blurMask Otsu" + strIdx, blurMask.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));
                    // blurMask = blurMask.Not();



                    /// b of LAB : | src - bg | x shadowMask
                    Image<Gray, Byte> threshIm = new Image<Gray, Byte>(srcLab.Split()[2].AbsDiff(bgLab.Split()[2]).Data);
                    // CvInvoke.cvShowImage("b" + strIdx, threshIm.Resize(0.25, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR));
                    //   threshIm = threshIm.AddWeighted(blurMask, 0.5, 0.5, 0);
                    // threshIm = threshIm.Mul(blurMask);
                    //threshIm = threshIm.Mul(shadowMask);
                    // Image<Gray, Byte> threshIm = new Image<Gray, Byte>(imSrc.AbsDiff(imBg).Convert<Gray, Byte>().Data);


                    Image<Gray, Byte> border = new Image<Gray, Byte>(threshIm.Width + 80, threshIm.Height + 80);

                    CvInvoke.cvCopyMakeBorder(threshIm, border, new Point(40, 40), Emgu.CV.CvEnum.BORDER_TYPE.CONSTANT, new MCvScalar(0));
                    threshIm = border;

                    threshIm = threshIm.ThresholdToZero(new Gray(10));

                    // CvInvoke.cvThreshold(threshIm, threshIm, 0, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY | Emgu.CV.CvEnum.THRESH.CV_THRESH_OTSU);
                    CvInvoke.cvThreshold(threshIm, threshIm, 29, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);

                    StructuringElementEx elementOpen = new StructuringElementEx(4, 4, 1, 1, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
                    threshIm = threshIm.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_OPEN, 3);

                    int size = 3;
                    /// ouverture 
                    elementOpen = new StructuringElementEx(size * 2 + 1, size * 2 + 1, size, size, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_ELLIPSE);
                    threshIm = threshIm.MorphologyEx(elementOpen, Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, 4);

                    threshIm.ROI = new Rectangle(40, 40, threshIm.Width - 80, threshIm.Height - 80);
                    threshTot.ROI = new Rectangle(rois[i].X, rois[i].Y, threshIm.Width, threshIm.Height);

                    threshIm.CopyTo(threshTot);
                }

                threshTot.ROI = new Rectangle(maxLeft, maxUp, maxRight - maxLeft, maxBottom - maxUp);
                imSrc.ROI = new Rectangle(maxLeft, maxUp, maxRight - maxLeft, maxBottom - maxUp);
                imBg.ROI = new Rectangle(maxLeft, maxUp, maxRight - maxLeft, maxBottom - maxUp);

                Image<Gray, Byte> res = new Image<Gray, Byte>(maxRight - maxLeft, maxBottom - maxUp);
                threshTot.CopyTo(res);

                return res;
            }
            return null;
        }
    }
}

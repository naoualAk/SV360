using Emgu.CV;
using Emgu.CV.Structure;
using SV360.Data;
using SV360.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace SV360.Image_Processing
{

    /// <summary>
    /// Classes qui pourrait utilisé pour la detection d'haploid
    /// </summary>
    class HaploidIP : ImageProcessing
    {
        public const int NBOFVIEW = 4;
        public const float EPSILON_CONV = (float) 1e-3;
        public const int Kgerme = 3;
        public const int Kgmm = 2;
        public const int genSize = 50;
        public override bool process(Seed s)
        {
            //Starting Haploid/diploid computing:
            Debug.WriteLine("Haploid seeds processing starts.");

            // Initialization:
            #region Initialization
            List<View> views = new List<View>();
            int[][] CoGs = new int[NBOFVIEW][];
            List<List<double>> data2save = new List<List<double>>();
            int sizeX = 0;
            int sizeY = 0;
            List<Image<Gray, Byte>> ViewsSrc = new List<Image<Gray, Byte>>();
            #endregion

            // Preparing images:
            #region Preparing images
            foreach (Acquisition acq in s.ImageCollection)
            {
                if (acq.Wavelength == WAVELENGTH.RED630)
                {
                    views = acq.Views;
                }
            }
                    //TODO: need to improve exception handling
            if (views == null)
                throw new Exception("no view available.", null);
            #endregion
            // Germe Detection:
            #region Germe Detection
            for (int i = 0; i < NBOFVIEW; i++)
            {
                ViewsSrc.Add(views[i].Image.Convert<Gray, Byte>());
                CoGs[i] = HaploidUtils.GermeDetection(ViewsSrc[i], views[i].Mask, EPSILON_CONV);

                if ((CoGs[i][1] + genSize) > ViewsSrc[i].Width || (CoGs[i][1] - genSize) < 1)
                    sizeX = Math.Min(ViewsSrc[i].Width - CoGs[i][1] - 1, CoGs[i][1] - 1);
                else
                    sizeX = genSize;
                if ((CoGs[i][0] + genSize) > ViewsSrc[i].Height || (CoGs[i][0] - genSize) < 1)
                    sizeY = Math.Min(ViewsSrc[i].Width - CoGs[i][0] - 1, CoGs[i][0] - 1);
                else
                    sizeY = genSize;

                ViewsSrc[i].ROI = new Rectangle(CoGs[i][0] - sizeX, CoGs[i][1] - sizeY, sizeX, sizeY);
            }
            #endregion
            // Features Extraction:
            #region Features Extraction
            for (int i = 0; i < NBOFVIEW; i++)
            {
                data2save.Add(HaploidUtils.FeaturesExtraction(ViewsSrc[i], views[i].Mask));
            } 
            #endregion

            return false;
        }
    }
}

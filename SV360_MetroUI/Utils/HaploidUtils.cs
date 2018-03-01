using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning;
//using processPerformance;
using System.Diagnostics;

namespace SV360.Utils
{
    /// <summary>
    /// "HaploidUtils" is a static class where utils function were developped to make Haploid image processing function easier to read.
    /// </summary>
    public static class HaploidUtils
    {
        /* Histogram processing code:*/
        /// <summary>
        /// This function computes the image histogramm, accordingly to its mask. 
        /// </summary>
        /// <param name="im"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static DenseHistogram imhist(Image<Gray, Byte> im, Image<Gray, Byte> mask)
        {
            DenseHistogram hist = new DenseHistogram(256, new RangeF(0, 255));
            hist.Calculate<Byte>(new Image<Gray, Byte>[] { im }, false, mask);

            return hist;
        }
        /// <summary>
        /// This function smouthes the histogram based on AR linear filter.
        /// </summary>
        /// <param name="hist"></param>
        /// <param name="SmoothFilterOrder"></param>
        /// <returns></returns>
        public static DenseHistogram SmoothHistogram(DenseHistogram hist, uint SmoothFilterOrder)
        {
            
            float[] t_hist = new float[256];

            if (SmoothFilterOrder < 2)
                return hist;

            // Histogram smouthing:
            for (int i = 0; i < SmoothFilterOrder-1; i++)
            {
                t_hist[i] = (float)hist.MatND.ManagedArray.GetValue(i);
            }

            for(int i = (int) SmoothFilterOrder-1; i < 256; i++)
            {
                for(int j = 0; j < SmoothFilterOrder; j++)
                {
                    t_hist[i] = t_hist[i] + (float)hist.MatND.ManagedArray.GetValue(i-j) / (float)SmoothFilterOrder;
                }
                t_hist[i] = (float) Math.Floor(t_hist[i]);
            }

            DenseHistogram histm = new DenseHistogram(256, new RangeF(0, 255));
            t_hist.CopyTo(histm.MatND.ManagedArray, 0);

            return histm;
        }
        /// <summary>
        /// This function makes a list of data generated from a histogram.
        /// </summary>
        /// <param name="hist"></param>
        /// <returns></returns>
        public static List<float> DatafromHistogram(DenseHistogram hist)
        {
            List<float> l = new List<float>();
            List<float> t_l = new List<float>();

            for (int i = 0; i < hist.MatND.ManagedArray.Length; i++)
            {
                for( int j = 0; j < (float) hist.MatND.ManagedArray.GetValue(i); j++)
                {
                    t_l.Add((float)i);
                }
                l.AddRange(t_l);
                t_l.Clear();
            }

            return l;
        }
        /// <summary>
        /// This function makes a list of data generated from a histogram. The return format makes it to be used by Gaussian Mixture Model functions in "Accord.MachineLearning".
        /// </summary>
        /// <param name="hist"></param>
        /// <returns></returns>
        public static double[][] DatafromHistogram2(DenseHistogram hist)
        {
            List<float> l = DatafromHistogram(hist);
            double[][] l2 = new double[l.Count][];
            for( int i = 0; i < l.Count; i++)
            {
                l2[i] = new double[1];
                l2[i][0] = l[i];
            }

            return l2;

        }
        /// <summary>
        /// This function sorts from lower values to higher values and returns the index of each values sorted.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int[] sort<T>(T[] data) where T : System.IComparable
        {
            int size = data.Length;
            if (size <= 1)
                return (new int[] { 0 });


            T transfert = data[0];
            int tempInd = 0;
            int[] ind = new int[size];

            for (int i = 0; i < size; i++)
            {
                ind[i] = i;
            }

                for (int i = 0; i < size - 1; i++)
            {
                if (data[i].CompareTo(data[i + 1]) > 0)
                {
                    transfert = data[i];
                    tempInd = ind[i];

                    ind[i] = ind[i + 1];
                    data[i] = data[i + 1];

                    data[i + 1] = transfert;
                    ind[i + 1] = tempInd;

                    if (i > 0)
                    {
                        i = i - 2;
                    }
                }
            }

            return ind;
        }
        /// <summary>
        /// Germe detection based on diploid seeds detection and their kernel. It returns the germe center.
        /// </summary>
        /// <param name="im"></param>
        /// <param name="mask"></param>
        /// <param name="epsilon_conv"></param>
        /// <param name="Kg"></param>
        /// <param name="filterOrder"></param>
        /// <returns></returns>
        public static int[] GermeDetection(Image<Gray,Byte> im, Image<Gray,Byte> mask, float epsilon_conv = (float) 1e-2, int Kg = 3, uint filterOrder = 3)
        {
            DenseHistogram hist = imhist(im, mask);
            DenseHistogram histm = SmoothHistogram(hist, filterOrder);
            double[][] data = DatafromHistogram2(histm);
            
            GaussianMixtureModel GMM = new GaussianMixtureModel(Kg);
            GMM.Compute(data, epsilon_conv);
            // mean, std and alpha
            List<double> mean = new List<double>();
            List<double> std = new List<double>();
            List<double> alpha = new List<double>();
            for (int i = 0; i < GMM.Gaussians.Count; i++)
            {
                mean.Add(GMM.Gaussians[i].Mean[0]);
                std.Add(Math.Sqrt(GMM.Gaussians[i].Covariance[0, 0]));
                alpha.Add(GMM.Gaussians[i].Proportion);
            }

            int[] ind = sort<double>(mean.ToArray());
            double[] thres = new double[Kg-1];

            // Looking for Optimal thresholds:
            for(int k = 0; k < Kg-1; k++)
            {
                double a = Math.Pow(1/std[ind[k]],2) - Math.Pow(1 / std[ind[k+1]],2);
                double b = mean[ind[k+1]] / Math.Pow(std[ind[k+1]],2) - mean[ind[k]] / Math.Pow(std[ind[k]],2);
                double c = Math.Pow(mean[ind[k]] / std[ind[k]],2) - Math.Pow(mean[ind[k+1]] / std[ind[k+1]],2) - 2*Math.Log(alpha[ind[k]] * std[ind[k+1]] / (alpha[ind[k+1]] * std[ind[k]]));
                double delta = b*b-a*c;
                if (delta < 0)
                    delta = 0;
                
                double thres1 = (-b + Math.Sqrt(delta)) / a;
                double thres2 = (-b - Math.Sqrt(delta)) / a;

                if (thres1 > 255 || thres1 < 0)
                    thres[k] = thres2;
                else
                    thres[k] = thres1;
            }
            Array.Sort(thres);

            // Center of gravity computing:
            double n = 0, x = 0, y = 0;
            for (int i = 0; i < im.Size.Height; i++)
            {
                for (int j = 0; j < im.Size.Width; j++)
                {
                    if (im[i, j].Intensity < thres[0])
                    {
                        x += i;
                        y += j;
                        n++;
                    }
                }
            }
            x = Math.Floor(x / n);
            y = Math.Floor(y / n);

            return (new int[] { (int) x, (int) y });
        }
        /// <summary>
        /// This function extracts features from Gaussian Mixture Model.
        /// </summary>
        /// <param name="im"></param>
        /// <param name="mask"></param>
        /// <param name="epsilon_conv"></param>
        /// <param name="Kmog"></param>
        /// <param name="filterOrder"></param>
        /// <returns></returns>
        public static List<double> FeaturesExtraction(Image<Gray, Byte> im, Image<Gray, Byte> mask, float epsilon_conv = (float) 1e-6, int Kmog = 2, uint filterOrder = 3)
        {
            DenseHistogram hist = imhist(im, mask);
            DenseHistogram histm = SmoothHistogram(hist, filterOrder);
            double[][] data = DatafromHistogram2(histm);

            GaussianMixtureModel GMM = new GaussianMixtureModel(Kmog);
            GMM.Compute(data, epsilon_conv);

            List<double> feat = new List<double>();
            for (int i = 0; i < GMM.Gaussians.Count; i++)
            {
                feat.Add(GMM.Gaussians[i].Mean[0]);
                feat.Add(Math.Sqrt(GMM.Gaussians[i].Covariance[0, 0]));
                feat.Add(GMM.Gaussians[i].Proportion);
            }

            return feat;
        }
    }
}

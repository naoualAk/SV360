using Emgu.CV;
using Emgu.CV.Structure;
using SV360.Data;
using SV360.IHM.Elements.Seuils;
using SV360.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Image_Processing
{

    /// <summary>
    /// Classe permettant d'extraire toute les données morphologiques grace à une graine. 
    /// </summary>
    public class MorphoIP : ImageProcessing
    {
        Sorting sorting = Sorting.Instance();

        /// <summary>
        /// non utilisé
        /// </summary>
        static int nb = 0;


        /// <summary>
        /// Met à jour les paramètres morphologiques d'une graine, en utilisant ses vues (views) 
        /// Le process doit être utilisé après un pre process
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public override bool process(Seed s)
        {

            if (s.ImageCollection.Count < 1 || s.ImageCollection[0].Views.Count != 2) return false;


            /// Calcul du coefficient miroir par rapport à la vue tapis
            float coefMirrorView = (s.ImageCollection[0].Views[0].Roi.Height / (float)s.ImageCollection[0].Views[1].Roi.Height);
            // float coefMirrorView = 1;

            s.CoefMirror = coefMirrorView;
            //Coef
            // float cX = 95F, cY = 92F, cZ = 92F;

            // Coef mm/pixel
            float cX = 85.6F, cY = 96.2F, cZ = 85.2F;


            float widthCaliper = 0, thicknessCaliper = 0, lengthCaliper = 0;
           

            //   double coefY = 0.95;

            Vector2[] vMirror = RotatingCaliper(s.ImageCollection[0].Views[0].ConvexHull);
            vMirror[0].X /= coefMirrorView; vMirror[0].
                Y /= coefMirrorView;
            vMirror[1].X /= coefMirrorView; vMirror[1].Y /= coefMirrorView;


            Vector2[] vBelt = RotatingCaliper(s.ImageCollection[0].Views[1].ConvexHull);

            Vector3[] v3D = new Vector3[4];
            v3D[0] = new Vector3(0, vMirror[1].Y, vMirror[1].X); // height for mirror view
            v3D[1] = new Vector3(0, vMirror[0].Y, vMirror[0].X); // width for mirror view
            v3D[2] = new Vector3(vBelt[1].X, vBelt[1].Y, 0); // height for belt view
            v3D[3] = new Vector3(vBelt[0].X, vBelt[0].Y, 0); // width for belt view

            for (int i = 0; i < v3D.Length; i++)
            {
                v3D[i].X /= cX;
                v3D[i].Y /= cY;
                v3D[i].Z /= cZ;
            }

            Array.Sort(v3D, (x, y) => ((double)y.Length()).CompareTo(x.Length())); // higher

            // determination of thickness and length with higher and smaller dimension
            float lengthX = v3D[0].X, lengthY = v3D[0].Y, lengthZ = v3D[0].Z;
            lengthCaliper = v3D[0].Length();

            thicknessCaliper = v3D[3].Length();
            float thicknessX = v3D[3].X, thicknessY = v3D[3].Y, thicknessZ = v3D[3].Z;

            // determination of width with Dot
            double pScal1;
            pScal1 = Vector3.Dot(Vector3.Normalize(v3D[1]), Vector3.Normalize(v3D[0]));
            pScal1 += Vector3.Dot(Vector3.Normalize(v3D[1]), Vector3.Normalize(v3D[3]));
            pScal1 /= 2;

            double pScal2;
            pScal2 = Vector3.Dot(Vector3.Normalize(v3D[2]), Vector3.Normalize(v3D[0]));
            pScal2 += Vector3.Dot(Vector3.Normalize(v3D[2]), Vector3.Normalize(v3D[3]));
            pScal2 /= 2;

            float widthX, widthY, widthZ;
            if (Math.Abs(pScal1) > Math.Abs(pScal2))
            {
                widthCaliper = v3D[2].Length();
                widthX = v3D[2].X; widthY = v3D[2].Y; widthZ = v3D[2].Z;
            }
            else
            {
                widthCaliper = v3D[1].Length();
                widthX = v3D[1].X; widthY = v3D[1].Y; widthZ = v3D[1].Z;
            }


            Debug.WriteLine("coefMirrorView={0}", coefMirrorView);
            Debug.WriteLine("vue 0: {0:0.00}x{1:0.00}\nvue 1: {2:0.00}x{3:0.00}", vMirror[1].Length(), vMirror[0].Length(), vBelt[1].Length(), vBelt[0].Length());

            s.VWidth = new Vector3(widthX * cX, widthY * cY, widthZ * cZ);
            s.VLength = new Vector3(lengthX * cX, lengthY * cY, lengthZ * cZ);
            s.VThickness = new Vector3(thicknessX * cX, thicknessY * cY, thicknessZ * cZ);

            /// Si le coefficient miroir n'est pas aberrant
            if (coefMirrorView > 0.75 && coefMirrorView < 1.2)
            {
                s.Width = (float)widthCaliper;
                s.Thickness = (float)thicknessCaliper;
                s.Length = (float)lengthCaliper;
                DecisionMaking(s);
            }
            else
            {
                s.Width = 0;
                s.Thickness = 0;
                s.Length = 0;
                s.NumClass = Seed.ENumClass.undefined;
                Debug.WriteLine("CoefMirror not valid : " + coefMirrorView);
                return false;
            }

            //Random rnd = new Random();
            // s.NumClass = rnd.Next(0, 2);

            /*       if (s != null && s.ImageCollection.Count > 0)
                   {
                       for (int i = 0; i < s.ImageCollection[0].Views.Count; i++)
                       {
                           s.ImageCollection[0].Views[i].Image.Save(@"C:\Users\damien.delhay\Documents\Travail\images-test\graine" + nb  + "-" + i+ ".bmp");
                       }
                   }

                   Random rnd = new Random();
                   int div = 4;
                   s.Width = rnd.Next(4*div, 8 * div)/(float)div; 
                   s.Thickness = rnd.Next(4 * div, 6 * div) / (float)div;
                   s.Length = rnd.Next(5 * div, 11 * div) / (float)div;
                   s.NumClass = rnd.Next(0, 2);

                   nb++;

                    */

            return true;
        }

        /// <summary>
        /// Met à jour la classe de la graine en fonction du choix de seuillage fait par l'utilisateur
        /// </summary>
        /// <param name="s"></param>
        private void DecisionMaking(Seed s)
        {
            if (sorting == null || sorting.Criterias == null || sorting.Criterias.Count < 1)
                s.NumClass = Seed.ENumClass.class1;
            for (int i = 0; i < sorting.Criterias.Count; i++)
            {
                if (RespectCriterias(sorting.Criterias[i], s))
                {
                    s.NumClass = (Seed.ENumClass)i + 1;
                    return;
                }
            }
        }

        private bool RespectCriterias(List<Criteria> criterias, Seed s)
        {
            for (int i = 0; i < criterias.Count; i++)
            {
                if (!criterias[i].Respect(s)) return false;
            }
            return true;
        }


        private int[] GetXExtremePoints(Seq<Point> convexHull)
        {
            if (convexHull.Total < 0) return null;

            int ymin = convexHull[0].Y, ymax = convexHull[0].Y;
            int[] idx = new int[2];
            idx[0] = convexHull[0].X;
            idx[1] = convexHull[0].X;

            for (int i = 0; i < convexHull.Total; i++)
            {
                if (ymin > convexHull[i].Y)
                {
                    ymin = convexHull[i].Y;
                    idx[0] = convexHull[i].X;
                }
                if (ymax < convexHull[i].Y)
                {
                    ymax = convexHull[i].Y;
                    idx[1] = convexHull[i].X;
                }
            }

            return idx;
        }



        /// <summary>
        /// Algo du pied à coulisse rotatif grace à une forme englobante
        /// </summary>
        /// <param name="convexHull">forme englobante</param>
        /// <returns>vector[0] = minLength, vector[1] = maxLength</returns>
        private Vector2[] RotatingCaliper(Seq<Point> convexHull)
        {
            int[] idExtremePoints = idExtremePoint(convexHull);
            int pA = idExtremePoints[0], pB = idExtremePoints[1];
            double rotatedAngle = 0;
            double minLength = double.PositiveInfinity;
            double maxLength = 0;


            Vector2[] vectors = new Vector2[2];

            Point pTemp1 = new Point(), pTemp2 = new Point();

            Vector2 caliperA = new Vector2(1, 0);
            Vector2 caliperB = new Vector2(-1, 0);
            caliperA = Vector2.Normalize(caliperA);
            caliperB = Vector2.Normalize(caliperB);

            while (rotatedAngle < Math.PI)
            {
                Vector2 egdeA = new Vector2(convexHull[(pA + 1) % convexHull.Total].X - convexHull[pA].X, convexHull[(pA + 1) % convexHull.Total].Y - convexHull[pA].Y);
                Vector2 egdeB = new Vector2(convexHull[(pB + 1) % convexHull.Total].X - convexHull[pB].X, convexHull[(pB + 1) % convexHull.Total].Y - convexHull[pB].Y);

                double angleA = _2D.angleRad(egdeA, caliperA);
                double angleB = _2D.angleRad(egdeB, caliperB);
                double length = 0;

                caliperA = _2D.rotate(caliperA, Math.Min(angleA, angleB));
                caliperB = _2D.rotate(caliperB, Math.Min(angleA, angleB));
                caliperA = Vector2.Normalize(caliperA);
                caliperB = Vector2.Normalize(caliperB);

                if (angleA <= angleB)
                {
                    pA = (pA + 1) % convexHull.Total;
                    if (pB != pA)
                    {
                        length = _2D.DistancePointToVector(convexHull[pB], convexHull[pA], caliperA, out pTemp2);
                        pTemp1 = convexHull[pB];
                    }
                }
                else
                {
                    pB = (pB + 1) % convexHull.Total;
                    if (pB != pA)
                    {
                        length = _2D.DistancePointToVector(convexHull[pA], convexHull[pB], caliperB, out pTemp1);
                        pTemp2 = convexHull[pA];
                    }
                }

                rotatedAngle = rotatedAngle + Math.Min(angleA, angleB);

                if (length < minLength && pB != pA)
                {
                    minLength = length;
                    vectors[0].Y = pTemp2.Y - pTemp1.Y;
                    vectors[0].X = pTemp2.X - pTemp1.X;
                }
                if (length > maxLength && pB != pA)
                {
                    maxLength = length;
                    vectors[1].Y = pTemp2.Y - pTemp1.Y;
                    vectors[1].X = pTemp2.X - pTemp1.X;
                }
            }

            return vectors;
        }

        /// <summary>
        /// Calcule la longueur et largeur d'une forme avec l'algo rotating caliper
        /// </summary>
        /// <param name="convexHull">forme englobante</param>
        /// <param name="_height">longueur</param>
        /// <param name="_width">largeur</param>
        private void rotCaliper(Seq<Point> convexHull, out double _height, out double _width)
        {
            int[] idExtremePoints = idExtremePoint(convexHull);
            int pA = idExtremePoints[0], pB = idExtremePoints[1];
            double rotatedAngle = 0;
            double minLength = double.PositiveInfinity;
            double maxLength = 0;

            Point pTemp1 = new Point(), pTemp2 = new Point();

            Vector2 caliperA = new Vector2(1, 0);
            Vector2 caliperB = new Vector2(-1, 0);
            caliperA = Vector2.Normalize(caliperA);
            caliperB = Vector2.Normalize(caliperB);

            while (rotatedAngle < Math.PI)
            {
                Vector2 egdeA = new Vector2(convexHull[(pA + 1) % convexHull.Total].X - convexHull[pA].X, convexHull[(pA + 1) % convexHull.Total].Y - convexHull[pA].Y);
                Vector2 egdeB = new Vector2(convexHull[(pB + 1) % convexHull.Total].X - convexHull[pB].X, convexHull[(pB + 1) % convexHull.Total].Y - convexHull[pB].Y);

                double angleA = _2D.angleRad(egdeA, caliperA);
                double angleB = _2D.angleRad(egdeB, caliperB);
                double width = 0;

                caliperA = _2D.rotate(caliperA, Math.Min(angleA, angleB));
                caliperB = _2D.rotate(caliperB, Math.Min(angleA, angleB));
                caliperA = Vector2.Normalize(caliperA);
                caliperB = Vector2.Normalize(caliperB);

                if (angleA <= angleB)
                {
                    pA = (pA + 1) % convexHull.Total;
                    if (pB != pA)
                    {
                        width = _2D.DistancePointToVector(convexHull[pB], convexHull[pA], caliperA, out pTemp2);
                        pTemp1 = convexHull[pB];
                    }
                }
                else
                {
                    pB = (pB + 1) % convexHull.Total;
                    if (pB != pA)
                    {
                        width = _2D.DistancePointToVector(convexHull[pA], convexHull[pB], caliperB, out pTemp1);
                        pTemp2 = convexHull[pA];
                    }
                }

                rotatedAngle = rotatedAngle + Math.Min(angleA, angleB);

                if (width < minLength && pB != pA)
                {
                    minLength = width;
                }
                if (width > maxLength && pB != pA)
                {
                    maxLength = width;
                }
            }

            _height = maxLength;
            _width = minLength;
        }

        /// <summary>
        /// Recherche des points le plus haut et le plus bas pour commencer le rotating caliper
        /// </summary>
        /// <param name="convexHull"></param>
        /// <returns>int[0]= le plus haut, idx[1]=le plus bas</returns>
        private int[] idExtremePoint(Seq<Point> convexHull)
        {

            if (convexHull.Total < 0) return null;

            int ymin = convexHull[0].Y, ymax = convexHull[0].Y;
            int[] idx = new int[2];

            for (int i = 0; i < convexHull.Total; i++)
            {
                if (ymin > convexHull[i].Y)
                {
                    ymin = convexHull[i].Y;
                    idx[0] = i;
                }
                if (ymax < convexHull[i].Y)
                {
                    ymax = convexHull[i].Y;
                    idx[1] = i;
                }
            }

            return idx;
        }
    }
}

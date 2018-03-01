using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Utils
{
    /// <summary>
    /// Classe permettant d'effectuer des calculs sur un plan 2D, avec des fonctions sur les vecteurs, des fonctions trigonométriques.
    /// </summary>
    public static class _2D
    {
        
        public static double length2P(Point p1, Point p2)
        {
            return length2P(new PointF(p1.X, p1.Y), new PointF(p2.X, p2.Y));
        }

        /// <summary>
        /// Longueur entre deux points
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static double length2P(PointF p1, PointF p2)
        {
            return Math.Sqrt((double)Math.Abs(p2.X - p1.X) * Math.Abs(p2.X - p1.X) + Math.Abs(p2.Y - p1.Y) * Math.Abs(p2.Y - p1.Y));
        }

        /// <summary>
        /// Rotation d'un vecteur en fonction d'un angle en rad
        /// </summary>
        /// <param name="v"></param>
        /// <param name="angleRad"></param>
        /// <returns></returns>
        public static Vector2 rotate(Vector2 v, double angleRad)
        {

            float X = v.X;
            float Y = v.Y;
            float cos = (float)Math.Cos(angleRad);
            float sin = (float)Math.Sin(angleRad);

            return new Vector2(X * cos - Y * sin, X * sin + Y * cos);
        }

        /// <summary>
        /// Angle entre deux vecteurs
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double angleRad(Vector2 v1, Vector2 v2)
        {
            Vector2 v = Vector2.Normalize(v1);

            float a = Vector2.Dot(v, v2);
            float b = v.Length();
            float c = v2.Length();
            float d = Vector2.Dot(v, v2) / (v.Length() * v2.Length());

            if (d > 1) d = 1;
            else if (d < -1) d = -1;


            double angle = Math.Acos(d);


            return angle > Math.PI ? angle - Math.PI : angle;
        }

        /// <summary>
        /// Distance entre un point pt et un segment d'origine p1 de direction vect
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="p1"></param>
        /// <param name="vect"></param>
        /// <param name="closest">point le plus proche de pt appartenant au segment</param>
        /// <returns></returns>
        public static double DistancePointToSegment(PointF pt, PointF p1, Vector2 vect, out PointF closest)
        {

            PointF p2 = new PointF(p1.X + vect.X, p1.Y + vect.Y);
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) /
                (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Distance entre un point pt et un segment d'origine p1 de direction vect
        /// </summary>
        /// <param name="pt"></param>
        /// <param name="p1"></param>
        /// <param name="vect"></param>
        /// <returns></returns>
        public static double DistancePointToVector(PointF pt, PointF p1, Vector2 vect)
        {
            PointF closest;
            PointF p2 = new PointF(p1.X + vect.X, p1.Y + vect.Y);

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;

            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) /
                (dx * dx + dy * dy);

            closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
            dx = pt.X - closest.X;
            dy = pt.Y - closest.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }


        /// <summary>
        ///  Calculate distance between one point and one vector.
        ///   And Returns the point of intersection
        /// </summary>
        /// <param name="pt"> point</param>
        /// <param name="p1"> origin vector</param>
        /// <param name="vect"> vector</param>
        /// <param name="pClosest">intersect point of distance point/vector</param>
        /// <returns> distance in pixel between point and vector </returns>
        public static double DistancePointToVector(PointF pt, PointF p1, Vector2 vect, out Point pClosest)
        {
            PointF closest = new PointF();

            float dx = vect.X;
            float dy = vect.Y;

            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                pClosest = new Point((int)closest.X, (int)closest.Y);
                return Math.Sqrt(dx * dx + dy * dy);
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) /
                (dx * dx + dy * dy);

            closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
            dx = pt.X - closest.X;
            dy = pt.Y - closest.Y;

            pClosest = new Point((int)closest.X, (int)closest.Y);

            return Math.Sqrt(dx * dx + dy * dy);
        }


    }
}

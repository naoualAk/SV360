using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Data
{

    /// <summary>
    /// view of one side of seed
    /// </summary>
    public class View : IDisposable
    {
        /// <summary> minimal image of view </summary>
        Image<Rgb, Byte> image;

        /// <summary> mask of object on the view </summary>
        Image<Gray, Byte> mask;

        /// <summary> mask of contour object on the view </summary>
        Image<Gray, Byte> maskContour;

        /// <summary> contour object on the view</summary>
        Contour<Point> contour;

        /// <summary> contour with less points than 'contour' on the view</summary>
        Contour<Point> approxContour;

        /// <summary>
        /// Convexhull (contour convexe ou englobant) d'une forme
        /// </summary>
        Seq<Point> convexHull;

        /// <summary> roi = size of image</summary>
        Rectangle roi;

        /// <summary> id of view with respect to the horizontal position. 0=left  higher=right  </summary>
        int id;

        /// <summary>
        /// Image brute de la vue
        /// </summary>
        public Image<Rgb, byte> Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }


        /// <summary>
        /// Mask de la vue (255 ou 0)
        /// </summary>
        public Image<Gray, byte> Mask
        {
            get
            {
                return mask;
            }

            set
            {
                mask = value;
            }
        }

        /// <summary>
        /// Mask du contour (255 ou 0) 
        /// Seul le contour est en blanc.
        /// </summary>
        public Image<Gray, byte> MaskContour
        {
            get
            {
                return maskContour;
            }

            set
            {
                maskContour = value;
            }
        }

        /// <summary>
        /// Contour de la forme
        /// </summary>
        public Contour<Point> Contour
        {
            get
            {
                return contour;
            }

            set
            {
                contour = value;
            }
        }

        // ROI de la vue
        public Rectangle Roi
        {
            get
            {
                return roi;
            }

            set
            {
                roi = value;
            }
        }

        /// <summary>
        /// contour approximatif de la formes
        /// </summary>
        public Contour<Point> ApproxContour
        {
            get
            {
                return approxContour;
            }

            set
            {
                approxContour = value;
            }
        }

        /// <summary>
        /// ID de la vue
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }


        /// <summary>
        /// Getter setter de Convexhull (contour convexe ou englobant) d'une forme
        /// </summary>
        public Seq<Point> ConvexHull
        {
            get
            {
                return convexHull;
            }

            set
            {
                convexHull = value;
            }
        }

        /// <summary>
        /// Nettoie et efface de la mémoire les images et masques de la vue.
        /// </summary>
        public void Dispose()
        {
          
            if (image != null)
                    image.Dispose();
            if (mask != null)
                mask.Dispose();
            if (maskContour != null)
                maskContour.Dispose();
        }
    }
}

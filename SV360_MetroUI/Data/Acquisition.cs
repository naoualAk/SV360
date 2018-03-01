using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Data
{
    /// <summary>
    /// enumération des différentes longueurs d'ondes possibles
    /// </summary>
    public enum WAVELENGTH : uint
    {
        /// <summary>
        /// RGB (par défaut)
        /// </summary>
        RGB = 0,
        /// <summary>
        /// RED630
        /// </summary>
        RED630 = 630,
        /// <summary>
        /// GREEN 525
        /// </summary>
        GREEN525 = 525,
        /// <summary>
        /// blue 405
        /// </summary>
        BLUE405 = 405
    };

    /// <summary>
    /// Classe qui contient toutes les données liées à une acquisition.
    /// Chaque acquisition possède une longueur d'onde, image brute, et une liste de vue (exemple : 2 pr convoyeur, 4 pour vision globale)
    /// </summary>
    public class Acquisition : IDisposable
    {
        private Image<Rgb, Byte> image;
        private List<View> views;
        private WAVELENGTH wavelength;

        /// <summary>
        /// Setter Getter de l'image brute
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
        /// Setter getter de la liste des vues 
        ///      exemple : 
        ///         2 vues pr convoyeur, 
        ///         4 vues pour vision globale
        /// </summary>
        public List<View> Views
        {
            get
            {
                return views;
            }
        }

        /// <summary>
        /// Getter setter longueur d'onde correspondant à l'acquisition
        /// </summary>
        public WAVELENGTH Wavelength
        {
            get
            {
                return wavelength;
            }

            set
            {
                wavelength = value;
            }
        }

        /// <summary>
        /// Cstor: 
        ///     Set l'image brute
        /// </summary>
        /// <param name="imSrc">image brute nécéssaire pour avoir une acquisition</param>
        public Acquisition(Image<Rgb, Byte> imSrc)
        {
            views = new List<View>();
            Image = imSrc;
        }

        /// <summary>
        /// Permet d'ajouter une vue à liste des vues
        /// </summary>
        /// <param name="view">vue à ajouter</param>
        /// <returns>true: ajout réussi
        /// false : ajout non réussi</returns>
        public bool AddView(View view)
        {
            if (view != null)
            {
                views.Add(view);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Supprime et nettoie l'image et les vues de la mémoire 
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < views.Count; i++)
            {
                views[i].Dispose();
            }

            if (image != null)
                image.Dispose();
        }

    }
}

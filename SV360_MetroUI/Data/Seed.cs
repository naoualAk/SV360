using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Data
{
    /// <summary>
    /// Classe contenant toutes les données relative à une graines.
    /// Chaque seed possède une liste d'acquistions, une classe de tri, les caractéristiques morphologiques et d'anomalies
    /// </summary>
    public class Seed
    {

        // liste d'acquisition
        private List<Acquisition> imageCollection;

        // private int numClass;   // numClass 1 to 4, if 0 is indetermined

        /// <summary>
        /// Enumération des valeurs de classe de tri
        /// Les classes de tri sont paramétrés avant le tri par l'utilisateur dans l'onglet Seuillage
        /// </summary>
        public enum ENumClass : int
        {
            /// <summary>
            /// Indéfini (bac le plus à gauche)
            /// </summary>
            undefined = 0,
            /// <summary>
            /// Classe 1 (même sortie que undefined)
            /// </summary>
            class1 = 1,
            /// <summary>
            /// Classe 2
            /// </summary>
            class2 = 2,
            /// <summary>
            /// Classe 3
            /// </summary>
            class3 = 3,
            /// <summary>
            /// Classe 4 (bac le plus à droite)
            /// </summary>
            class4 = 4
        };

        /// <summary>
        /// nombre de classe possibles
        /// </summary>
        public static int nbClass = 5;


        ENumClass numClass = ENumClass.class1;


        private float length;
        private float width;
        private float thickness;

        private float coefMirror;

        private Vector3 vLength;
        private Vector3 vWidth;
        private Vector3 vThickness;

        private bool isHaploid;
        private bool isGwaned;
        private bool isDamaged;


        /// <summary>
        /// Getter Setter sur la longuer de la graine
        /// </summary>
        public float Length
        {
            get
            {
                return length;
            }

            set
            {
                if (value >= 0)
                    this.length = (float)Math.Round(value, 2, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// Getter Setter sur la largeur
        /// </summary>
        public float Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value >= 0)
                    this.width = (float)Math.Round(value, 2, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// Getter Setter sur l'épaisseur
        /// </summary>
        public float Thickness
        {
            get
            {
                return thickness;
            }

            set
            {
                if (value >= 0)
                    this.thickness = (float)Math.Round(value, 2, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// Getter Setter sur l'état haploid
        /// </summary>
        public bool IsHaploid
        {
            get
            {
                return isHaploid;
            }

            set
            {
                this.isHaploid = value;
            }
        }

        /// <summary>
        /// Getter Setter sur l'état rongé
        /// </summary>
        public bool IsGwaned /*rongé*/
        {
            get
            {
                return isGwaned;
            }

            set
            {
                this.isGwaned = value;
            }
        }

        /// <summary>
        /// Getter Setter sur l'état fissuré
        /// </summary>
        public bool IsDamaged /*fissuré*/
        {
            get
            {
                return isDamaged;
            }

            set
            {
                this.isDamaged = value;
            }
        }

        /// <summary>
        /// Getter Setter sur la liste d'acquisition
        /// </summary>
        public List<Acquisition> ImageCollection
        {
            get
            {
                return imageCollection;
            }

            set
            {
                imageCollection = value;
            }
        }


        /// <summary>
        /// Getter Setter sur le numéro de la classe
        /// </summary>
        public ENumClass NumClass
        {
            get
            {
                return numClass;
            }

            set
            {
                numClass = value;
            }
        }

        /// <summary>
        /// Getter Setter sur le vecteur de la longueur
        /// </summary>
        public Vector3 VLength
        {
            get
            {
                return vLength;
            }

            set
            {
                vLength = value;
            }
        }


        /// <summary>
        /// Getter Setter sur le vecteur de la largeur
        /// </summary>
        public Vector3 VWidth
        {
            get
            {
                return vWidth;
            }

            set
            {
                vWidth = value;
            }
        }

        /// <summary>
        /// Getter Setter sur le vecteur de l'épaisseur
        /// </summary>
        public Vector3 VThickness
        {
            get
            {
                return vThickness;
            }

            set
            {
                vThickness = value;
            }
        }

        /// <summary>
        /// Getter Setter sur le coefficient miroir du convoyeur 
        /// (Utilisé pour le debug)
        /// </summary>
        public float CoefMirror
        {
            get
            {
                return coefMirror;
            }

            set
            {
                coefMirror = value;
            }
        }

        /* Add any parameters you need. */

        /// <summary>
        /// Cstor : 
        ///     mets tout les paramétre à 0. 
        ///     Classe à trier à 1.
        /// </summary>
        public Seed()
        {
            Initialize();
        }


        /// <summary>
        /// Cstor : 
        ///     mets tout les paramétre à 0. 
        ///     Classe à trier à 1.
        ///     Set la première acquisition
        /// </summary>
        /// <param name="v"></param>
        public Seed(Acquisition v)
        {
            Initialize();
            AddAcquisition(v);
        }


        private void Initialize()
        {
            this.Length = 0;
            this.Width = 0;
            this.Thickness = 0;

            numClass = ENumClass.class1;

            this.IsHaploid = false;
            this.IsGwaned = false;
            this.IsDamaged = false;

            this.imageCollection = new List<Acquisition>();
        }


        /// <summary>
        /// Ajoute une acquisition à la graine.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool AddAcquisition(Acquisition v)
        {
            if (v != null)
            {
                this.ImageCollection.Add(v);
                return true;
            }
            else
                return false;
        }


        /// <summary>
        /// Supprime les acquisitions correspondant à une longueur d'onde
        /// </summary>
        /// <param name="wl">longueur d'onde à supprimer</param>
        /// <returns>true: suppression réussi
        /// false: suppresssion non réussi (aucune longueur d'onde trouvée à supprimer)</returns>
        public bool RemoveAcquisition(WAVELENGTH wl)
        {
            foreach (Acquisition acq in ImageCollection)
            {
                if (acq.Wavelength == wl)
                {
                    ImageCollection.Remove(acq);
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Nettoie et supprime de la mémoire la liste des acquistions.
        /// </summary>
        public void DisposeAcquisitions()
        {
            for (int i = 0; i < ImageCollection.Count; i++)
            {
                ImageCollection[i].Dispose();
            }
            ImageCollection.Clear();
        }
    }
}

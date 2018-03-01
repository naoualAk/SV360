using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Data
{

    /// <summary>
    /// Classe utilisé pour construire des tableaux de distributions sur la longueur, largeur et épaisseur
    /// </summary>
    class Batch
    {
        // liste des graines
        List<Seed> seeds;
        

        // tableau des distributions (axe des ordonnées)
        private int[] widthDistrib, thicknessDistrib, lengthDistrib;

        // tableau des pas(axe des abscisses)
        private float[] step;


        // nombre de graines comprises dans les dimmensions MIN MAX
        int cumul = 0;

        /// <summary>
        /// dimension maximal pris en compte
        /// </summary>
        const float MAX_DIM = 15;

        /// <summary>
        /// dimension minimal pris en compte
        /// </summary>
        const float MIN_DIM = 1;

        /// <summary>
        /// dimension minimal pris en compte
        /// </summary>
        const float STEP = 0.25F;

        /// <summary>
        /// calcule de l'idx maximum à atteindre par rapport au min max et step 
        /// </summary>
        const int MAX_IDX = (int)((MAX_DIM - MIN_DIM) / (STEP)) + 1;



        public int[] WidthDistrib
        {
            get
            {
                return widthDistrib;
            }

            set
            {
                widthDistrib = value;
            }
        }

        public int[] ThicknessDistrib
        {
            get
            {
                return thicknessDistrib;
            }

            set
            {
                thicknessDistrib = value;
            }
        }

        public int[] LengthDistrib
        {
            get
            {
                return lengthDistrib;
            }

            set
            {
                lengthDistrib = value;
            }
        }

        public int Cumul
        {
            get
            {
                return cumul;
            }

            set
            {
                cumul = value;
            }
        }

        public int MaxIdx
        {
            get
            {
                return MAX_IDX;
            }
        }

        public float[] Step
        {
            get
            {
                return step;
            }

            set
            {
                step = value;
            }
        }

        public float MaxDim
        {
            get
            {
                return MAX_DIM;
            }
        }

        public float MinDim
        {
            get
            {
                return MIN_DIM;
            }
        }


        /// <summary>
        /// Cstor : 
        ///     Calcule les distributions de la largeur, longueur epaisseur d'une liste de seeds
        /// </summary>
        /// <param name="seeds">liste de graines à calculer</param>
        public Batch(List<Seed> seeds)
        {

            // dimension beetween 1 to 15 mm
            WidthDistrib = new int[MAX_IDX];
            LengthDistrib = new int[MAX_IDX];
            ThicknessDistrib = new int[MAX_IDX];
            Step = new float[MAX_IDX];

            for (int i = 0; i < MAX_IDX; i++)
            {
                WidthDistrib[i] = LengthDistrib[i] = ThicknessDistrib[i] = 0;
                Step[i] = 1 + i * 0.25F;
            }

            for (int i = 0; i < seeds.Count; i++)
            {
                int iW = (int)((float)Math.Floor(seeds[i].Width * 4) - 4);
                int iT = (int)((float)Math.Floor(seeds[i].Thickness * 4) - 4);
                int iL = (int)((float)Math.Floor(seeds[i].Length * 4) - 4);

                if (IsInDimension(iW) && IsInDimension(iT) && IsInDimension(iL))
                {
                    Cumul++;
                    WidthDistrib[iW]++;
                    LengthDistrib[iL]++;
                    ThicknessDistrib[iT]++;
                }
            }


        }


        private bool IsInDimension(int i)
        {
            if (i >= 0 && i < MAX_IDX)
                return true;
            else return false;
        }

        public int MinIdxWidth()
        {
            for (int i = 0; i < MaxIdx; i++)
            {
                if (widthDistrib[i] != 0)
                    return i;
            }
            return 0;
        }
        public int MinIdxThickness()
        {
            for (int i = 0; i < MaxIdx; i++)
            {
                if (thicknessDistrib[i] != 0)
                    return i;
            }
            return 0;
        }
        public int MinIdxLength()
        {
            for (int i = 0; i < MaxIdx; i++)
            {
                if (lengthDistrib[i] != 0)
                    return i;
            }
            return 0;
        }

        public int MaxIdxWidth()
        {
            for (int i = MaxIdx - 1; i >= 0; i--)
            {
                if (widthDistrib[i] != 0)
                    return i;
            }
            return 0;
        }
        public int MaxIdxThickness()
        {
            for (int i = MaxIdx - 1; i >= 0; i--)
            {
                if (thicknessDistrib[i] != 0)
                    return i;
            }
            return 0;
        }
        public int MaxIdxLength()
        {
            for (int i = MaxIdx - 1; i >= 0; i--)
            {
                if (lengthDistrib[i] != 0)
                    return i;
            }
            return 0;
        }
    }
}

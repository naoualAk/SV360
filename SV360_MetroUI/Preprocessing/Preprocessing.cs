using Emgu.CV;
using Emgu.CV.Structure;
using SV360.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Preprocessing
{
    /// <summary>
    /// Classe permettant de faire la segmentation et le cropping des vues 
    /// </summary>
    public abstract class Preprocessing
    {

        /// <summary>
        /// preprocessing:
        /// 
        /// segmentation of seeds
        /// cropping of views
        /// </summary>
        /// <param name="acquisition">acquisition d'une image</param>
        /// <returns> true if succeeded, false if not</returns>
        public abstract bool preprocessing(Acquisition acquisition);
    }
}

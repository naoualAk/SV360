using SV360.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Image_Processing
{
    /// <summary>
    /// Classe asbtraite pour définir les processing des images
    /// </summary>
    public abstract class ImageProcessing
    {
        /// <summary>
        /// Méthode permettant d'effectuer un processing en fonction d'une graine.
        /// </summary>
        /// <param name="s">graine</param>
        /// <returns>true si réussi, false sinon</returns>
        public abstract bool process(Seed s);
    }
}

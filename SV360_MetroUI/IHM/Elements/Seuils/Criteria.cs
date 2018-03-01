using SV360.Data;
using SV360.Resources.lang;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SV360.IHM.Elements.Seuils
{

    /// <summary>
    /// Classe de donnée qui permet de structurer un critère. 
    /// Un critere est une caractéristiques liés à un comparateur (inférieur ou supérieur) et à une valeur 
    /// exemple : ( longueur inférieur à 6)
    /// 
    /// </summary>
    public class Criteria
    {

        /// <summary>
        /// Valeur du critere
        /// </summary>
        public float value;
        /// <summary>
        /// caractéristique du critère
        /// </summary>
        public Feature feature;
        /// <summary>
        /// Comparateur du critère
        /// </summary>
        public Comp comp;


        /// <summary>
        ///  Cstor : 
        ///     Instancie un critère, et ajoute sa caractéristique, son comparateur et sa valeur
        /// </summary>
        /// <param name="c"></param>
        /// <param name="cp"></param>
        /// <param name="v"></param>
        public Criteria(Feature c, Comp cp, float v)
        {
            feature = c;
            comp = cp;
            value = v;
        }

        /// <summary>
        ///  Renvoi ses données dans un string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string mess = ToStr(feature) + " " + ToStr(comp) + " " + value;
            return mess;
        }

        /// <summary>
        ///  renvoi un string en fonction de son élément comparateur
        /// </summary>
        /// <param name="comp"></param>
        /// <returns></returns>
        private string ToStr(Comp comp)
        {
            if (comp == Comp.inf)
                return " < ";
            else
                return " ≥ ";
        }

        /// <summary>
        /// renvoi un string en fonction de sa caractériques
        /// </summary>
        /// <param name="crit"></param>
        /// <returns></returns>
        private string ToStr(Feature crit)
        {

            if (crit == Feature.Width)
                return Lang.Text("width");
            else if (crit == Feature.Thickness)
                return Lang.Text("thickness");
            else
                return Lang.Text("length");
        }


        /// <summary>
        ///  Vérifie si une graine respecte le critère
        /// </summary>
        /// <param name="s">graine</param>
        /// <returns>true si le critere est respecté,  false sinon</returns>
        public bool Respect(Seed s)
        {
            float v;
            switch (feature)
            {
                case Feature.Width:
                    v = s.Width;
                    break;
                case Feature.Length:
                    v = s.Length;
                    break;
                case Feature.Thickness:
                    v = s.Thickness;
                    break;
                default:
                    v = 0;
                    break;
            }
            switch (comp)
            {
                case Comp.inf:
                    if (v < value && v>0)
                        return true;
                    else return false;

                case Comp.sup:
                    if (v >= value)
                        return true;
                    else return false;
                default:
                    return false;
            }

        }
    }
}

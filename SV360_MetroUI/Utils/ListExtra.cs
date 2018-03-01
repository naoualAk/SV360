using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SV360.Utils
{

    /// <summary>
    /// Classe permettant de réaliser des méthodes sur les listes : 
    ///  <list type="bullet">
    ///  <item>inversement de deux valeur dans la liste</item>
    ///  <item>calcul de l'écart type dans une liste</item>
    ///  </list>
    /// </summary>
    static public class ListExtra
    {

        /// <summary>
        /// Inverse deux valeurs dans une liste 
        /// </summary>
        /// <typeparam name="T">Type de la liste</typeparam>
        /// <param name="list">liste</param>
        /// <param name="index1">indice de la première valeur</param>
        /// <param name="index2">indice de la seconde valeur</param>
        public static void Swap<T>(this List<T> list, int index1, int index2)
        {
            T temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        /// <summary>
        /// Calcul l'écart type d'une liste
        /// </summary>
        /// <typeparam name="T">type de la liste </typeparam>
        /// <param name="list">liste</param>
        /// <param name="values">attribut à utiliser pour calculer l'écart type</param>
        /// <returns>écart type</returns>
        public static double StdDev<T>(this IEnumerable<T> list, Func<T, double> values)
        {
            // ref: http://stackoverflow.com/questions/2253874/linq-equivalent-for-standard-deviation
            // ref: http://warrenseen.com/blog/2006/03/13/how-to-calculate-standard-deviation/ 
            var mean = 0.0;
            var sum = 0.0;
            var stdDev = 0.0;
            var n = 0;
            foreach (var value in list.Select(values))
            {
                n++;
                var delta = value - mean;
                mean += delta / n;
                sum += delta * (value - mean);
            }
            if (1 < n)
                stdDev = Math.Sqrt(sum / (n - 1));

            return stdDev;
        }
    }
}


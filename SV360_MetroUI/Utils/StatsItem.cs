using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SV360.Utils
{
    /// <summary>
    /// Classe permettant de regrouper les données liées aux statistiques d'une liste
    /// </summary>
    public class StatsItem
    {
        ///Item average
        public double average { get; set; }
        ///Item max
        public double max { get; set; }
        ///Item min
        public double min { get; set; }
        ///Item standard deviation
        public double stdDeviation { get; set; }
        ///Item conuter
        public int counter { get; set; }
        ///Buffers
        private double sum;        
        private double sum2;

        /// <summary>
        /// Cstr: 
        /// Instancie et mets les données stats à 0
        /// </summary>
        public StatsItem()
        {
            counter = 0;
            average = 0;
            max = 0;
            min = 1000;
            stdDeviation = 0;            
            sum2 = 0;
        }

        ///Calculate statistics online
        public void Compute(double val)
        {
            counter++;
            sum += val;
            sum2 += Math.Pow(val, 2);
            average = Math.Round(sum / counter,2);
            stdDeviation = Math.Round(Math.Sqrt((sum2 / counter) - (Math.Pow(sum, 2) / Math.Pow(counter, 2))),2);
            if (val > max)
            {
                max = val;
            }
            if (val < min)
            {
                min = val;
            }
        }


        /// <summary>
        /// Renvoi les données statistiques dans un  string
        /// </summary>
        /// <returns>données statistiques</returns>
        public override string ToString()
        {
            return String.Format("Average = "+average+"\nMax = "+max+"\nMin = "+min+"\nStandard Deviation = "+stdDeviation);
        }
    }
}

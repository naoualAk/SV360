using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Automatism
{
    /// <summary>
    /// Type de donnée pouvant être lue
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Booléen
        /// </summary>
        Boolean,
        /// <summary>
        /// Integer
        /// </summary>
        Integer
    }

    /// <summary>
    /// Classe de donnée permettant de structurer les variables scannées dans l'automate par le pc
    /// </summary>
    public class ScannedVariable
    {

        /// <summary>
        /// Adresse dans l'automate
        /// </summary>
        public int addr { get; set; }

        

        public DataType valueType { get; set; }

        /// <summary>
        /// Valeur précédente
        /// </summary>
        public int oldValue { get; set; }

        /// <summary>
        /// Valeur actuelle
        /// </summary>
        public int currentValue { get; set; }
    }
}

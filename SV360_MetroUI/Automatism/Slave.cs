using ModbusTCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Automatism
{
    /// <summary>
    /// Classe abstraite qui est utilisé par les classes en relation avec l'automate 
    /// </summary>
    public abstract class Slave 
    {

        /// <summary>
        /// Master de la communication MODBUS
        /// </summary>
        protected Master master;
        /// <summary>
        /// Mot mémoire de l'esclave 
        ///     envoi   1 pour activer
        ///             0 pour désactiver
        /// </summary>
        protected ushort idSlave;


        /// <summary>
        /// Cstr : 
        ///     set master et idslave.
        /// </summary>
        /// <param name="master"></param>
        /// <param name="idSlave"></param>
        public Slave(Master master, ushort idSlave)
        {
            this.master = master;
            this.idSlave = idSlave;
        }

        /// <summary>
        /// Envoie (OnOff) 1 ou 0 dans l'adresse mot mémoire dédié à l'esclave.
        /// </summary>
        /// <param name="OnOff">Valeur à envoyer</param>
        protected void Send(bool OnOff)
        {
            master.WriteSingleCoils(1, 0, idSlave, OnOff);
        }

    }
}

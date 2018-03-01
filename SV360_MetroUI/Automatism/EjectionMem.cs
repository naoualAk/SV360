using ModbusTCP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Automatism
{
    /// <summary>
    /// Classe qui envoie les commandes à l'automate qui permet de piloter les ejections.
    /// </summary>
    class EjectionMem
    {
        protected Master master;
        enum Addr { Word = 3, SaveWord = 66 }

        /// <summary>
        /// Cstor : set la connexion Modbus.
        /// </summary>
        /// <param name="master">Connexion ModbusTCP avec l'automate</param>
        public EjectionMem(Master master)
        {
            this.master = master;
        }


        /// <summary>
        ///  Envoie la valeur dans la file des prochaines sorties à l'automate.
        /// </summary>
        /// <param name="value">Valeur de la prochaine sortie (doit être compris entre 0 et 3)</param>
        public void SetClass(int value)
        {
            if (value < 0 && value > 3)
            {
                Debug.WriteLine("The value must be between 0 and 3, so the system don't send the value");
                return;
            }
            else
            {
                
                // Préparation de l'envoi : conversion int en byte pour l'envoie
                byte[] intBytes = BitConverter.GetBytes(value);
                if (BitConverter.IsLittleEndian)
                    Array.Reverse(intBytes);
                byte[] mess = new byte[2];
                mess[0] = intBytes[2];
                mess[1] = intBytes[3];

                // envoi de la valeur à l'automate
                master.WriteSingleRegister(1, 0, (ushort)Addr.Word, mess);

                // envoi du bit ACK à l'automate. l'automate sauvegardera la valeur envoyé précédemment.
                master.WriteSingleCoils(1, 0, (ushort)Addr.SaveWord, true);

                Debug.WriteLine("EjectionMem : SetClass :" + value, "Automatism");
            }
        }
    }
}
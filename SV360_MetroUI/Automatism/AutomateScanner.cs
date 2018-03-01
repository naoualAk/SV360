using ModbusTCP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SV360.Automatism
{
    /// <summary>
    /// Classe qui permet de scruter les variables dans l'automate.
    /// </summary>
    public class AutomateScanner
    {
        protected Master master;

        public EventHandler<ScannedVariable> VariableChanged;

        private static List<ScannedVariable> scannedVariables;

        private Thread scannerThread;

        private AutomateScanner(Master master)
        {
            scannedVariables = new List<ScannedVariable>();
            this.master = master;
        }

        public bool Exist()
        {
            if (automateScanner == null)
                return false;
            else return true;
        }


        private static AutomateScanner automateScanner;

        public static AutomateScanner Instance(Master master = null)
        {
            if (automateScanner == null)
            {
                if (master == null)
                {
                    Debug.WriteLine("For the first time, master should be initialized");
                    return null;
                }      
                else automateScanner = new AutomateScanner(master);
            }

            return automateScanner;
        }

        public void RefreshVariablesThread()
        {

            scannerThread = new Thread(new ThreadStart(RefreshVariables));

        }

        private void RefreshVariables()
        {
            lock (scannedVariables)
            {

            }
        }

        private void ReadVariable(ScannedVariable scannedVariable)
        {
            ReadVariable(scannedVariable, master);         
        }

        /// <summary>
        /// Permet de lire une variable %M'X' ou %MW'X' de l'automate. 'X' étant l'adresse de la variable.
        /// </summary>
        /// <param name="scannedVariable">Variable à scanner</param>
        /// <param name="master">Master de communication avec l'automate</param>
        public static void ReadVariable(ScannedVariable scannedVariable, Master master )
        {

            if (master == null || scannedVariable == null) return;

            int sizeData = 0;
            if (scannedVariable.valueType == DataType.Boolean)
                sizeData = 1;
            else sizeData = 4;


            byte[] bytes = new byte[sizeData];

            if (scannedVariable.valueType == DataType.Boolean)
            {
                master.ReadCoils(1, 0, (ushort)scannedVariable.addr, (ushort)sizeData, ref bytes);
            }
            else master.ReadInputRegister(1, 0, (ushort)scannedVariable.addr, (ushort)sizeData, ref bytes);

            if (BitConverter.IsLittleEndian)
               Array.Reverse(bytes);
            try
            {
                int value;
                if (scannedVariable.valueType == DataType.Boolean)
                {
                    value = bytes[0];
                }
                else value = BitConverter.ToInt16(bytes, 6); 
                
                scannedVariable.oldValue = scannedVariable.currentValue;
                scannedVariable.currentValue = value;
               // if (scannedVariable.currentValue != scannedVariable.oldValue)
                  //  VariableChanged(this, scannedVariable);
            }
            catch
            {

            }

        }


        public bool AddScannedVariable(int address, DataType valueType)
        {
            int sizeData = 0;
            if (valueType == DataType.Boolean)
                sizeData = 1;
            else sizeData = 2;

            byte[] bytes = new byte[sizeData];
            master.ReadCoils(1, 0, (ushort)address, (ushort)sizeData, ref bytes);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            int value = BitConverter.ToInt32(bytes, 0);
            //master.WriteSingleCoils(1, 0, (ushort)Addr.SaveWord, true);

            scannedVariables.Add(new ScannedVariable() { addr = address, currentValue = value, oldValue = value, valueType = valueType });

            return true;
        }


    }
}

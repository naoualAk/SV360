using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Process_Tester.Utils
{
    class SVTimer
    {
        // External functions
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long freq);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long count);

        // Member variables
        long m_CounterFrequency, m_ResetTime;

        // Class contructor
        public SVTimer()
        {
            bool ret = QueryPerformanceFrequency(out m_CounterFrequency);
            Reset();
        }

        // Reset the timer
        public void Reset()
        {
            bool ret = QueryPerformanceCounter(out m_ResetTime);
        }

        public float GetTime()
        {
            return GetTime(false);
        }

        // Compute and return time
        public float GetTime(bool reset)
        {
            // Get current time
            long currentTime;
            bool ret = QueryPerformanceCounter(out currentTime);

            // Compute difference from last reset
            long diffTime = currentTime - m_ResetTime;

            // Reset time
            if (reset)
                m_ResetTime = currentTime;

            // Return time in seconds
            return (float)diffTime / (float)m_CounterFrequency;
        }

        public override string ToString()
        {
            return "Temps:" + GetTime()*1000 + "ms";
        }
    }
}

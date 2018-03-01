using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SV360.Utils
{
    //This class is a Singleton

    [Obsolete("Plus utilisé")]
    public class BatchStats
    {
        private static BatchStats batchStats;
        //Seeds area statistics
        public StatsItem areaStats { get; set; }
        //Seeds roundness statistics
        public StatsItem roundnessStats { get; set; }
        //Seeds perimeter statistics
        public StatsItem perimeterStats { get; set; }
        //Seeds length statistics
        public StatsItem lengthStats { get; set; }
        //Seeds thickness statistics
        public StatsItem thicknessStats { get; set; }
        //Seeds red color statistics
        public StatsItem redStats { get; set; }
        //Seeds green color statistics
        public StatsItem greenStats { get; set; }
        //Seeds blue color statistics
        public StatsItem blueStats { get; set; }


        protected BatchStats()
        {
            areaStats = new StatsItem();
            roundnessStats= new StatsItem();
            perimeterStats= new StatsItem();
            lengthStats= new StatsItem();
            thicknessStats = new StatsItem();
            redStats = new StatsItem();
            greenStats = new StatsItem();
            blueStats = new StatsItem();
        }

        public static BatchStats Instance()
        {
            if (batchStats == null)
            {
                batchStats = new BatchStats();
            }

            return batchStats;
        }
    
    }
}

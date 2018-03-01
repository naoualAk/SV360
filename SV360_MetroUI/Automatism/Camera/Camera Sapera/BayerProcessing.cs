using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DALSA.SaperaLT.SapClassBasic;

namespace SV360.Utils
{
    [Obsolete("Utilisé anciennement sur le proto (Version Emeric)")]
    class BayerProcessing : SapProcessing
    {
        private SapBayer bayer;

        // Constructor
        public BayerProcessing(SapBuffer pBuffers, SapBayer pBayer, SapProcessingDoneHandler pCallback, Object pContext)
            : base(pBuffers)
        {
            base.ProcessingDoneEnable = true;
            base.ProcessingDone += pCallback;
            base.ProcessingDoneContext = pContext;
            bayer = pBayer;
        }

        //Bayer conversion
        public override bool Run()
        {
            if (bayer.Enabled && bayer.SoftwareConversion)
            {
                bayer.Convert(base.Index);
            }

            return true;
        }
    }
}

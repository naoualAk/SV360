using SV360.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SV360.IHM.Elements.Analyse
{

    [Obsolete("Non utilisé")]
    class LabelResume : Label
    {
        Sorting sorting;
        public LabelResume()
        {
             sorting = Sorting.Instance(); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString("A", Font, new SolidBrush(ForeColor), 10, 10);
            e.Graphics.DrawString("B", new Font(Font.FontFamily, 20), new SolidBrush(ForeColor), 50, 10);
        }
    }
}

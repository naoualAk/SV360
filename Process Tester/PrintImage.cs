using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Process_Tester
{
    public partial class PrintImage : Form
    {
        public PrintImage(string path)
        {

            InitializeComponent();

            Image<Rgb, byte> im = new Image<Rgb, byte>(path);

            imageBox.Image = im;

        }
    }
}

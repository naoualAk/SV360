using SV360.Data;
using SV360.IHM.PageTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SV360.Data;
using SV360.IHM.Elements.Seuils;
using SV360.Utils;
using System.Diagnostics;

namespace IHM_TESTER
{
    public partial class Form1 : Form
    {

        List<Seed> seeds;
        Sorting sort;
        Random rand;

        private void SaveExcel(object sender, string path)
        {
            if (ExcelController.Write(seeds, path))
                MessageBox.Show("Le fichier s'est sauvegardé correctement");
        }

        public Form1()
        {
            InitializeComponent();

            sort = Sorting.Instance();
            List<List<Criteria>> criterias = new List<List<Criteria>>();
            criterias.Add(new List<Criteria> { new Criteria(Feature.Thickness, Comp.inf, 4.0F) });
            criterias.Add(new List<Criteria> { new Criteria(Feature.Thickness, Comp.sup, 4.0F) });
          //  criterias.Add(new List<Criteria> { new Criteria(Feature.Thickness, Comp.sup, 4.0F), new Criteria(Feature.Width, Comp.sup, 6.5F) });
           // criterias.Add(new List<Criteria> { new Criteria(Feature.Thickness, Comp.sup, 4.0F), new Criteria(Feature.Width, Comp.inf, 6.5F) });

            sort.Criterias = criterias;


            seeds = new List<Seed>();

             rand = new Random();
            for (int i = 0; i < 4000; i++)
            {
                Seed s = new Seed();

                double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
                double u2 = rand.NextDouble();
                double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                             Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                float rnd = 6 + 3.5F * (float)randStdNormal; //random normal(mean,stdDev^2)
    
                s.Width = rnd;

                u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
                u2 = rand.NextDouble();
                randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                            Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                rnd = 4.4F +3.5F * (float)randStdNormal; //random normal(mean,stdDev^2)
                s.Thickness = rnd;

                u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
                u2 = rand.NextDouble();
                randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                            Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
                rnd = 8 + 3.5F * (float)randStdNormal; //random normal(mean,stdDev^2)
                s.Length = rnd;

                DecisionMaking(s);

                if (i < 100)
                {
                    s.NumClass = 0;
                }

                seeds.Add(s);
            }

            /*   Seed sd = new Seed();
               sd.Width = 0; 
               seeds.Add(sd);*/

           /* 
            SortingUC userSorting = new SortingUC();
            panel1.Controls.Add(userSorting);
            userSorting.Dock = DockStyle.Fill;*/

            AnalysisUC userControlAnalyser = new AnalysisUC();
            //userControlAnalyser.SaveExcel += new EventHandler<string>(SaveExcel);
            panel1.Controls.Add(userControlAnalyser);
            userControlAnalyser.Dock = DockStyle.Fill;

            userControlAnalyser.Set(seeds, true);
        }

  

        private void DecisionMaking(Seed s)
        {
            if (sort == null || sort.Criterias == null || sort.Criterias.Count < 1)
            {
                s.NumClass = Seed.ENumClass.class1;
                return;
            }
            for (int i = 0; i < sort.Criterias.Count; i++)
            {
                if (RespectCriterias(sort.Criterias[i], s))
                {
                    s.NumClass = (Seed.ENumClass)i+1;
                    Debug.WriteLine(i + 1);
                    return;
                }
            }
        }

        private bool RespectCriterias(List<Criteria> criterias, Seed s)
        {
            for (int i = 0; i < criterias.Count; i++)
            {
                if (!criterias[i].Respect(s)) return false;
            }
            return true;
        }
    }
}


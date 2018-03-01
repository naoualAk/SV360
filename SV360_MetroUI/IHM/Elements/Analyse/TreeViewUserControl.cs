using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MetroFramework.Controls;
using SV360.Data;
using SV360.Utils;
using System.Diagnostics;

namespace SV360.IHM.Elements
{

    /// <summary>
    /// Classe qui permet d'afficher les données d'une liste de grains via un arbre
    /// </summary>
    public partial class TreeViewUserControl : MetroUserControl
    {
        class Node
        {
            public string Name { get; private set; }
            public string Column1 { get; private set; }

            public List<Node> Children { get; private set; }
            public Node(string name, string col1)
            {
                this.Name = name;
                this.Column1 = col1;
                this.Children = new List<Node>();
            }
        }

        // private fields
        private List<Node> data;
        private TreeListView treeListView;

        OLVColumn nameCol, valueCol;

        enum NumClass : int { general = -1, undefinied=0, class1 = 1, class2 = 2, class3 = 3, class4 = 4 };

        NumClass numClass;
        Node general;
        Node class1 ;
        Node class2 ;
        Node class3;
        Node class4 ;

        /// <summary>
        /// Cstor : 
        ///     
        /// </summary>
        /// <param name="seeds"></param>
        public TreeViewUserControl(List<Seed> seeds)
        {
            numClass = NumClass.general;

            InitializeComponent();
            AddTree();
            InitializeData(seeds);
            FillTree();

            treeListView.Expand(general);
            treeListView.Expand(class1);
            treeListView.Expand(class2);
            treeListView.Expand(class3);
            treeListView.Expand(class4);

            Resize += new EventHandler(UpdateUI);
        }

       [Obsolete("non utilsé")]
        public TreeViewUserControl(List<Seed> seeds, int numClass)
        {
            if (numClass > 4 || numClass < 0)
                this.numClass = NumClass.general;
            else this.numClass = (NumClass)numClass;

            InitializeComponent();
            AddTree();
            InitializeData(seeds);
            FillTree();

            treeListView.Expand(general);
            treeListView.Expand(class1);
            treeListView.Expand(class2);
            treeListView.Expand(class3);
            treeListView.Expand(class4);

            Resize += new EventHandler(UpdateUI);
        }

        private void UpdateUI(object sender, EventArgs e)
        {
            if (nameCol.Width > treeListView.Width)
            {
                nameCol.Width = 150;
            }

            valueCol.Width = treeListView.Width - nameCol.Width - 4;
        }



        // private methods
        private void FillTree()
        {
            // set the delegate that the tree uses to know if a node is expandable
            this.treeListView.CanExpandGetter = x => (x as Node).Children.Count > 0;
            // set the delegate that the tree uses to know the children of a node
            this.treeListView.ChildrenGetter = x => (x as Node).Children;

            // create the tree columns and set the delegates to print the desired object proerty
            nameCol = new BrightIdeasSoftware.OLVColumn("Name", "Name");
            nameCol.AspectGetter = x => (x as Node).Name;

            valueCol = new BrightIdeasSoftware.OLVColumn("Value", "Value");
            valueCol.AspectGetter = x => (x as Node).Column1;

            // add the columns to the tree
            this.treeListView.Columns.Add(nameCol);
            this.treeListView.Columns.Add(valueCol);

            nameCol.Width = 150;
            valueCol.Width = treeListView.Width - nameCol.Width;

            treeListView.ExpandAll();

            // set the tree roots
            this.treeListView.Roots = data;
        }


        /// <summary>
        /// Ajoute les données statistiques pour chaque caractéristiques morphologiques
        /// </summary>
        /// <param name="seeds"></param>
        /// <param name="node"></param>
        private void FillMorphoNode(IEnumerable<Seed> seeds, Node node)
        {
            if (seeds.Count<Seed>() < 1) return;

            var thickness = new Node("Epaisseur", "");
            thickness.Children.Add(new Node("Min", seeds.Min(s => s.Thickness).ToString("0.000")));
            thickness.Children.Add(new Node("Max", seeds.Max(s => s.Thickness).ToString("0.000")));
            thickness.Children.Add(new Node("Moyenne", seeds.Average(s => s.Thickness).ToString("0.000")));
            thickness.Children.Add(new Node("Ecart type", seeds.StdDev(s => s.Thickness).ToString("0.000")));

            var width = new Node("Largeur", "");
            width.Children.Add(new Node("Min", seeds.Min(s => s.Width).ToString("0.000")));
            width.Children.Add(new Node("Max", seeds.Max(s => s.Width).ToString("0.000")));
            width.Children.Add(new Node("Moyenne", seeds.Average(s => s.Width).ToString("0.000")));
            width.Children.Add(new Node("Ecart type", seeds.StdDev(s => s.Width).ToString("0.000")));


            var length = new Node("Longueur", "");
            length.Children.Add(new Node("Min", seeds.Min(s => s.Length).ToString("0.000")));
            length.Children.Add(new Node("Max", seeds.Max(s => s.Length).ToString("0.000")));
            length.Children.Add(new Node("Moyenne", seeds.Average(s => s.Length).ToString("0.000")));
            length.Children.Add(new Node("Ecart type", seeds.StdDev(s => s.Length).ToString("0.000")));

            var morphologie = new Node("Morphologie", "");
            morphologie.Children.Add(thickness);
            morphologie.Children.Add(width);
            morphologie.Children.Add(length);
            node.Children.Add(morphologie);
        }
         
        /// <summary>
        /// Initialise les données 
        /// </summary>
        /// <param name="seeds"></param>
        private void InitializeData(List<Seed> seeds)
        {
            general = new Node("General", "");

           // var pctTotal =
            general.Children.Add(new Node("% total", "100%"));
            general.Children.Add(new Node("Nombre de grains", seeds.Count.ToString()));
            FillMorphoNode(seeds, general);
            /*
             * var anomalies = new Node("Anomalies", "");
            anomalies.Children.Add(new Node("Saine", "100%"));
            anomalies.Children.Add(new Node("Eclatés", "0%"));
            anomalies.Children.Add(new Node("Fissurés", "0%"));
            general.Children.Add(anomalies);

    */
            class1 = new Node("Classe 1", "");
            FillClass(class1, NumClass.class1, seeds);

            class2 = new Node("Classe 2", "");
            FillClass(class2, NumClass.class2, seeds);

            class3 = new Node("Classe 3", "");
            FillClass(class3, NumClass.class3, seeds);

            class4 = new Node("Classe 4", "");
            FillClass(class4, NumClass.class4, seeds);


            data = new List<Node> { general, class1,class2, class3, class4 };
        }


        private void FillClass(Node nodeClass, NumClass nc, List<Seed> seeds)
        {

          /*  var anomalies = new Node("Anomalies", "");
            anomalies.Children.Add(new Node("Saine", "100%"));
            anomalies.Children.Add(new Node("Eclatés", "0%"));
            anomalies.Children.Add(new Node("Fissurés", "0%"));*/

            var seedsClass = from s in seeds
                             where s.NumClass == (Seed.ENumClass)nc
                             select s;

            int cnt = seedsClass.Count();
            nodeClass.Children.Add(new Node("% total", ((float)cnt * 100 / seeds.Count).ToString("0.0")+ "%"));
            nodeClass.Children.Add(new Node("Nombre de grains", cnt.ToString()));
            FillMorphoNode(seedsClass, nodeClass);
            /*if(cnt>0)
                nodeClass.Children.Add(anomalies);*/
        }
   
        private void AddTree()
        {
            treeListView = new TreeListView();

            treeListView.Dock = DockStyle.Fill;
            treeListView.HeaderStyle = ColumnHeaderStyle.Nonclickable;

            this.Controls.Add(treeListView);
        }
    }
}


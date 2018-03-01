using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using SV360.Resources;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using SV360.Resources.lang;

namespace SV360.IHM.Elements.Seuils
{

    /// <summary>
    /// Enumération de l'élément comparateur
    /// </summary>
    public enum Comp {
        /// <summary>
        /// inférieur
        /// </summary>
        inf,
        /// <summary>
        /// supérieur ou égal
        /// </summary>
        sup
    };

    /// <summary>
    /// Enumération des caractéristiques
    /// </summary>
    public enum Feature {
        /// <summary>
        /// largeur
        /// </summary>
        Width,
        /// <summary>
        /// longueur
        /// </summary>
        Length,
        /// <summary>
        /// épaisseur
        /// </summary>
        Thickness
    };


    /// <summary>
    /// Threshold est un Usercontrol qui :
    /// <list type="bullet">
    /// <item>affiche ses données à l'écran  </item>
    /// <item>gere ses boutons</item>
    /// <item>gere ses données </item>
    /// <item>est lié à ses enfants et son parent.</item>
    /// </list>
    /// Il s'agit d'un noeud d'un arbre.
    /// </summary>
    public partial class Threshold : MetroUserControl
    {
        /// <summary>
        /// Evenement d'une classe créée
        /// </summary>
        public event EventHandler<List<List<Criteria>>> ClassesCreated;

        /// <summary>
        /// element comparateur
        /// </summary>
        public Comp comp;

        /// <summary>
        /// fils droite (supérieur ou égale) et gauche (inférieur)
        /// </summary>
        public Threshold sonLeft, sonRight;

        /// <summary>
        /// parent 
        /// </summary>
        public Threshold parent;

        /// <summary>
        /// nombre d'étages dans l'arbre
        /// </summary>
        public static int nbEtages = 0;

        /// <summary>
        ///  valeur du seuil
        /// </summary>
        public float value;

        /// <summary>
        /// caractéristiques du seuil
        /// </summary>
        public Feature feature;


        List<Criteria> criterias = new List<Criteria>();

        /// event next
        public event EventHandler NextClicked;

        /// attribut seulement utilisé par le seuil origine 
        List<List<Criteria>> criteriasByClass;

        /// arbre est rempli
        bool treeIsFill = false;

        /// le seuil est en vie
        bool isAlive = false;


        /// <summary>
        /// Création du seuil origin
        /// </summary>
        public Threshold()
        {
            InitializeComponent();
            btCancel.Enabled = false;

            criterias = new List<Criteria>();

            btStart.UseSelectable = false;
            tableLayoutPanel2.BackColor = SVColor.clouds;
            Init();
        }


        /// <summary>
        /// Cstor des seuils non-origines  en fonction d'un parent et de son élément comparateur (corrélé au fils droit ou gauche). Instancie les données graphqiues
        ///     
        /// </summary>
        /// <param name="comp">élément comparateur</param>
        /// <param name="parent"> parent du seuil</param>
        public Threshold(Comp comp, Threshold parent)
        {
            this.parent = parent;

            InitializeComponent();
            UpdateLanguage();
            this.comp = comp;
            btCancel.Enabled = false;
            criterias = new List<Criteria>();
            if (parent != null)
            {
                btStart.Text = Lang.Text("introduce_threshold");
                UpdateTextChild();
            }
            btStart.UseSelectable = false;
            tableLayoutPanel2.BackColor = SVColor.clouds;
            Init();
        }


        /// <summary>
        ///  le seuil origine retourne la liste des listes des criteres 
        /// </summary>
        /// <returns></returns>
        internal List<List<Criteria>> RootGetClasses()
        {
            if (parent != null) return null;
            List<List<Criteria>> criterias = new List<List<Criteria>>();
            AddCritere(criterias);

            return criterias;
        }

        /// <summary>
        /// Update la langue
        /// </summary>
        internal void UpdateLanguage()
        {
            if (sonLeft != null)
            {
                sonLeft.UpdateLanguage();
                sonRight.UpdateLanguage();
                btStart.Text = Lang.Text("selected_threshold") + " : " + ToStr(feature) + " " + value.ToString();
            }
            else
            {
                if (Root().TreeIsFill)
                    Root().DisplayClass();
                else
                    btStart.Text = Lang.Text("introduce_threshold");
            }

            boxCritere.Items.Clear();
            boxCritere.Items.AddRange(new String[] { Lang.Text("thickness"), Lang.Text("width"), Lang.Text("length") });
        }

        /// <summary>
        /// Affiche au lieu des seuils, le numéro de la classe pour les enfants sans enfants
        /// </summary>
        /// <param name="i">numéro initial des classes</param>
        /// <returns></returns>
        public int DisplayClass(int i = 1)
        {
            if (HasSon())
            {
                i = sonRight.DisplayClass(i);
                i = sonLeft.DisplayClass(i);
                return i;
            }
            else if (!HasSon())
            {
                AfficheClasse(i);
                return i + 1;
            }
            return i;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="criteres"></param>
        /// <param name="idx"> start idx</param>
        /// <returns></returns>
        int AddCritere(List<List<Criteria>> criteres, int idx = 1)
        {
            // si aucun parent critere clear
            if (parent == null) criteres.Clear();

            //si il existe un fils
            if (HasSon())
            {
                idx = sonRight.AddCritere(criteres, idx);
                sonLeft.AddCritere(criteres, idx);
                return (idx + 1);
            }
            else if (!HasSon()) // si aucun fils
            {
                //Ajouter la liste de criteres 
                //AfficheClasse(idx);
                criteres.Add(criterias);
                return (idx + 1);
            }
            return idx + 1;
        }


        /// <summary>
        /// Obtient le frere du seuil
        /// </summary>
        /// <returns> frere du seuil</returns>
        Threshold Brother()
        {
            if (parent == null)
                return null;
            if (parent.sonLeft == this)
                return parent.sonRight;
            else return parent.sonLeft;
        }



        private bool commandIsOpen;

        /// <summary>
        /// Arbre est rempli (nombre d'étage sup ou egale à 3 ou classe validé par l'utilisateur) ? 
        /// </summary>
        public bool TreeIsFill
        {
            get
            {
                return treeIsFill;
            }

            set
            {
                treeIsFill = value;
            }
        }


        private bool HasSon()
        {
            if (sonLeft != null && sonLeft.isAlive)
                return true;
            else return false;
        }


        private void Init()
        {
            btStart.BackColor = SVColor.alizarin;
        }


        private bool isSonLeft()
        {
            if (parent != null)
            {
                if (parent.sonLeft == this)
                    return true;
                else return false;
            }
            return false;
        }

        /// <summary>
        /// Initialise les graphiques
        /// </summary>
        public void Initialize()
        {
            btStart.ForeColor = System.Drawing.Color.White;
            boxDecimal.Enabled = true;
            boxCritere.Enabled = true;
            boxUnit.Enabled = true;
            btCancel.Visible = true;
            tableLayoutPanel2.BackColor = SVColor.clouds;
            Visible = true;
            isAlive = true;
        }

        /// <summary>
        /// Affiche au lieu des paramètres des seuils la classe 
        /// </summary>
        /// <param name="a">numéro de la classe</param>
        public void AfficheClasse(int a)
        {
            btStart.ForeColor = System.Drawing.Color.White;
            btStart.Text = Lang.Text("Class") + " " + a;
            btStart.BackColor = SVColor.emerald;
            tableLayoutPanel2.BackColor = SVColor.clouds;
        }

        /// <summary>
        /// Qd bouton suivant appuyé, 
        ///     Création ou affichage des fils. 
        ///     Vérification si le nombre de classes possibles est inférieur à 5.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Next_Click(object sender, EventArgs e)
        {
            if (boxUnit.Text != "" && boxCritere.Text != "" && boxDecimal.Text != "")
            {
                value = float.Parse(boxUnit.Text) + float.Parse("0" + boxDecimal.Text);
                setCritere(boxCritere.Text);
                NextClicked(this, EventArgs.Empty);
                if (parent != null)
                {
                    if (!isCorrectSeuil())
                    {
                        if (sonLeft != null)
                        {
                            sonRight.Visible = false;
                            sonRight.isAlive = false;
                            sonLeft.Visible = false;
                            sonLeft.isAlive = false;

                            boxCritere.ForeColor = System.Drawing.Color.Red;
                            boxDecimal.SelectedIndex = -1;
                            boxUnit.SelectedIndex = -1;
                            tableLayoutPanel2.BackColor = System.Drawing.Color.Red;
                            // MessageBox.Show(" Introduisez une bonne valeur ");
                            return;
                        }
                    }
                }
                if (!sonLeft.isAlive)
                {
                    tableLayoutPanel2.Size = new System.Drawing.Size(tableLayoutPanel2.Size.Width, 0);
                    tableLayoutPanel2.Visible = false;
                    commandIsOpen = false;
                    tableLayoutPanel2.BackColor = SVColor.clouds;
                    boxDecimal.Enabled = false;
                    boxCritere.Enabled = false;
                    boxUnit.Enabled = false;
                    nbEtages++;
                    btCancel.Enabled = true;
                    sonLeft.Initialize();
                    sonRight.Initialize();
                    UpdateTextAllBranches();
                    btStart.BackColor = System.Drawing.Color.White;
                    btStart.Text = Lang.Text("selected_threshold") + " : " + ToStr(feature) + " " + value.ToString();
                    btStart.ForeColor = SVColor.alizarin;
                    tableLayoutPanel2.BackColor = SVColor.clouds;
                    Root().TreeIsFill = false;
                    Root().UpdateStartBt();

                    if (nbEtages >= 3)
                    {

                        List<List<Criteria>> criteriasByClass = new List<List<Criteria>>();
                        Root().AddCritere(criteriasByClass);
                        Root().DisplayClass();

                        Root().TreeIsFill = true;
                        Root().criteriasByClass = criteriasByClass;

                        CommandVisibility(false);

                        //MessageBox.Show(" Classes remplies ");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Retourne le seuil origine
        /// </summary>
        /// <returns>seuil origine</returns>
        private Threshold Root()
        {
            if (parent != null)
                return parent.Root();
            else return this;
        }

        private void printCriterias(Threshold thresh, int classe)
        {
            Debug.WriteLine(Lang.Text("Class") + " " + classe + ":");

            for (int i = 0; i < thresh.criterias.Count; i++)
                Debug.WriteLine(thresh.criterias[i]);
        }

        /// vérifier si le seuil selectionné est acceptable ou non
        private bool isCorrectSeuil()
        {
            if (parent != null)
            {
                for (int i = 0; i < criterias.Count; i++)
                {
                    if (criterias.Count == 1)
                    {
                        if ((criterias[i].feature == feature && (comp == Comp.inf && value >= criterias[i].value))
                   || (criterias[i].feature == feature && (comp == Comp.sup && value <= criterias[i].value)))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (parent.parent.feature == parent.feature && parent.feature == feature)
                        {
                            if ((parent.comp == Comp.inf && comp == Comp.sup && (value >= parent.parent.value || parent.value >= parent.parent.value || value <= parent.value))
                                || (parent.comp == Comp.sup && comp == Comp.inf && (value <= parent.parent.value || parent.value <= parent.parent.value || value >= parent.value)))
                            {
                                return false;
                            }
                        }
                        if ((parent.parent.feature == feature) && ((parent.comp == Comp.inf && value >= parent.parent.value) || (parent.comp == Comp.sup && value <= parent.parent.value)))
                        {
                            return false;
                        }
                        if ((parent.feature == feature && (comp == Comp.inf && value >= parent.value))
                            || (parent.feature == feature && (comp == Comp.sup && value <= parent.value)))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }


        private void UpdateTextAllBranches()
        {
            if (parent != null)
                parent.UpdateTextAllBranches();
            else
            {
                sonRight.UpdateTextChild();
                sonLeft.UpdateTextChild();
            }
        }

        private void UpdateTextChild()
        {
            criterias.Clear();
            if (parent != null)
            {

                for (int i = 0; i < parent.criterias.Count; i++)
                {
                    if (parent.feature == parent.criterias[i].feature && (
                        (comp == Comp.inf && parent.value < parent.criterias[i].value)
                        || (comp == Comp.sup && parent.value > parent.criterias[i].value)
                        ))
                        continue;
                    criterias.Add(parent.criterias[i]);
                }
                criterias.Add(new Criteria(parent.feature, comp, parent.value));

                if (!isCorrectSeuil())
                {
                    if (HasSon())
                    {
                        nbEtages--;
                        sonLeft.VisibleTree(false);
                        sonRight.VisibleTree(false);
                        CommandVisibility(true);
                    }
                }

            }
            if (sonLeft != null)
            {
                sonLeft.UpdateTextChild();
                sonRight.UpdateTextChild();
            }
        }

        /// <summary>
        /// Rend visible ou non le noeud et ses enfants
        /// </summary>
        /// <param name="OnOff"></param>
        public void VisibleTree(bool OnOff)
        {
            Visible = OnOff;
            isAlive = OnOff;

            if (HasSon())
            {
                nbEtages--;
                sonLeft.VisibleTree(OnOff);
                sonRight.VisibleTree(OnOff);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (HasSon())
            {
                CommandVisibility(true);
                UpdateBack();
                sonLeft.UpdateBackChild();
                sonRight.UpdateBackChild();
                nbEtages--;
                Root().TreeIsFill = false;
                Root().UpdateStartBt();
            }
            btCancel.Enabled = false;
        }

        /// <summary>
        /// Affichage du premier bouton du seuil 
        /// </summary>
        public void UpdateStartBt()
        {
            if (!Root().treeIsFill)
            {
                if (HasSon())
                {
                    sonLeft.UpdateStartBt();
                    sonRight.UpdateStartBt();
                }
                else
                {
                    if (commandIsOpen)
                    {
                        btStart.Text = Lang.Text("introduce_threshold");
                        btStart.BackColor = SVColor.clouds;
                        btStart.ForeColor = System.Drawing.Color.Black;
                    }
                    else
                    {
                        btStart.Text = Lang.Text("introduce_threshold");
                        btStart.BackColor = SVColor.alizarin;
                        btStart.ForeColor = System.Drawing.Color.White;
                    }
                }
            }

        }

        /// <summary>
        /// Update graphique: rend actif les combobox de choix pour l'utilisateur
        /// </summary>
        public void back1()
        {
            btStart.Text = Lang.Text("introduce_threshold");
            btStart.BackColor = SVColor.clouds;
            btStart.ForeColor = System.Drawing.Color.Black;
            boxDecimal.SelectedIndex = -1;
            boxUnit.SelectedIndex = -1;
            boxDecimal.Enabled = true;
            boxCritere.Enabled = true;
            boxUnit.Enabled = true;
            btCancel.Enabled = false;
            tableLayoutPanel2.BackColor = SVColor.clouds;
        }

        /// <summary>
        /// Update Graphique: rend invisible le tableau de commande du usercontrol
        /// </summary>
        public void back2()
        {
            btStart.Text = Lang.Text("introduce_threshold");
            btStart.BackColor = SVColor.alizarin;
            btStart.ForeColor = System.Drawing.Color.White;
            tableLayoutPanel2.Visible = false;
            commandIsOpen = false;
            tableLayoutPanel2.BackColor = SVColor.clouds;
        }

        /// <summary>
        /// Update l'arbre en fonction des données dans le usercontrol
        /// </summary>
        public void UpdateBack()
        {
            if (parent != null)
            {
                if (parent.parent != null)
                {
                    if (parent.parent.sonLeft.boxDecimal.Text != "")
                    {
                        parent.parent.sonRight.back2();
                    }
                    if (parent.parent.sonRight.boxDecimal.Text != "")
                    {
                        parent.parent.sonLeft.back2();
                    }
                    if (parent.sonLeft.boxDecimal.Text != "")
                    {
                        parent.sonLeft.back1();
                        parent.sonRight.back2();
                    }
                    if (parent.sonRight.boxDecimal.Text != "")
                    {
                        parent.sonRight.back1();
                        parent.sonLeft.back2();
                    }
                }

                if (parent != null && parent.parent == null)
                {
                    if ((parent.sonLeft.boxDecimal.Text != "") && (parent.sonRight.boxDecimal.Text == ""))
                    {
                        parent.sonLeft.back1();
                        parent.sonRight.back2();
                    }
                    else if ((parent.sonRight.boxDecimal.Text != "") && (parent.sonLeft.boxDecimal.Text == ""))
                    {
                        parent.sonRight.back1();
                        parent.sonLeft.back2();
                    }
                    else if ((parent.sonRight.boxDecimal.Text != "") && (parent.sonLeft.boxDecimal.Text != ""))
                    {
                        if (sonLeft.btStart.Text == (Lang.Text("Class") + " 1"))
                        {
                            parent.sonRight.sonLeft.back2();
                            parent.sonRight.sonRight.back2();
                            back1();
                        }
                        else
                        {
                            parent.sonLeft.sonLeft.back2();
                            parent.sonLeft.sonRight.back2();
                            back1();
                        }
                    }
                }
            }
            else
            {
                back1();
            }
        }

        /// <summary>
        /// Update graphique des enfants
        /// </summary>
        private void UpdateBackChild()
        {
            Visible = false;
            isAlive = false;
            btStart.Text = Lang.Text("introduce_threshold");
            btStart.BackColor = SVColor.alizarin;
            tableLayoutPanel2.Visible = false;
            commandIsOpen = false;
            boxUnit.SelectedIndex = -1;
            boxDecimal.SelectedIndex = -1;
            boxCritere.SelectedIndex = -1;
            if (HasSon())
            {
                nbEtages--;
                sonLeft.UpdateBackChild();
                sonRight.UpdateBackChild();
            }
        }

        /// <summary>
        /// Rend visible les commandes utilisateur d'un seuil
        /// </summary>
        /// <param name="OnOff"></param>
        private void CommandVisibility(bool OnOff)
        {
            if (parent != null)
                parent.CommandVisibility(OnOff);
            else
            {
                sonLeft.CommandVisibilityChild(OnOff);
                sonRight.CommandVisibilityChild(OnOff);
            }
        }

        /// <summary>
        /// REnd visible les commandes utilisateurs des enfants
        /// </summary>
        /// <param name="OnOff"></param>
        private void CommandVisibilityChild(bool OnOff)
        {
            if (HasSon())
            {
                sonLeft.CommandVisibilityChild(OnOff);
                sonRight.CommandVisibilityChild(OnOff);
            }
            else
            {
                btCancel.Visible = OnOff;
                boxCritere.Visible = OnOff;
                boxUnit.Visible = OnOff;
                boxDecimal.Visible = OnOff;
                tableLayoutPanel2.Visible = OnOff;
                commandIsOpen = OnOff;
            }
        }

        private string ToStr(Comp comp)
        {
            if (comp == Comp.inf)
                return " ≤ ";
            else
                return " > ";
        }

        private string ToStr(Feature crit)
        {
            if (crit == Feature.Width)
                return Lang.Text("width");
            else if (crit == Feature.Thickness)
                return Lang.Text("thickness");
            else
                return Lang.Text("length");
        }

        /// <summary>
        /// Setcritere en fonction de la combobox rempli par l'utilisateur
        /// </summary>
        /// <param name="str"></param>
        public void setCritere(string str)
        {
            if (str == Lang.Text("width"))
                feature = Feature.Width;
            else if (str == Lang.Text("thickness"))
                feature = Feature.Thickness;
            else if (str == Lang.Text("length"))
                feature = Feature.Length;
            else Debug.WriteLine("Problems of language traduction for criteres setter");
        }

        /// <summary>
        /// Appuie sur le premiere bouton pour ouvrir le panneau de commande utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btStart_Click(object sender, EventArgs e)
        {
            if (commandIsOpen)
            {
                tableLayoutPanel2.Visible = false;
                commandIsOpen = false;
            }
            else
            {
                tableLayoutPanel2.Visible = true;
                commandIsOpen = true;
            }
            if (boxDecimal.Text != "" || btStart.Text == Lang.Text("Class") + " 1" || btStart.Text == Lang.Text("Class") + " 2" || btStart.Text == Lang.Text("Class") + " 3" || btStart.Text == Lang.Text("Class") + " 4")
                return;
            else
            {
                btStart.Text = Lang.Text("introduce_threshold");
                btStart.BackColor = SVColor.clouds;
                btStart.ForeColor = System.Drawing.Color.Black;// ajout couleur
            }
            if (!commandIsOpen)
            {
                btStart.BackColor = SVColor.alizarin;
                btStart.ForeColor = System.Drawing.Color.White;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            tableLayoutPanel1.BackColor = SVColor.clouds;
        }
    }
}

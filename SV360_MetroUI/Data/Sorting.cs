using SV360.IHM.Elements.Seuils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV360.Data
{


    /// <summary>
    /// Sorting is a singleton.
    /// La classe sorting contient les données qui permettent d'enregistrer la liste de liste des critères pour permettre la selectionner la classe d'une graine 
    /// </summary>
    public class Sorting
    {

       private static Sorting sorting;

        // criterias of sorting
        private List<List<Criteria>> criterias;

        /// <summary>
        /// Cstor: 
        ///     Seulement si aucun objet crée
        /// </summary>
       private Sorting()
       {
            criterias = new List<List<Criteria>>();
       }


        /// <summary>
        /// Getter Setter de la liste de liste de critere
        /// Par défaut, la taile de la 1ere liste ne peut pas dépasser 4.
        /// 
        /// La liste est construit comme un arbre, il ne peut donc être défini que d'une certaine façon. 
        ///     
        ///<para/>  Exemple de liste critere : 
        ///<para/>  Arbre de classification :
        ///<para/>          Largeur  7  
        ///<para/>        ------------
        ///<para/>   inf  ||        || sup
        ///<para/>        \/        \/
        ///<para/>        C3      Epaisseur 5
        ///<para/>               -------------
        ///<para/>          inf  ||         || sup
        ///<para/>               \/         \/        
        ///<para/>               C2         C1
        ///<para/>  
        ///<para/>  Modèle dans la liste: 
        ///<para/>      List[0] (C1)    : ListCritere[0] (Critère 1) = Largeur sup  7
        ///<para/>                      : ListCritere[1] (Critère 2) = Epaisseur sup  5
        ///<para/>                      
        ///<para/>      List[1] (C2)    : ListCritere[0] (Critère 1) = Largeur sup  7
        ///<para/>                      : ListCritere[1] (Critère 2) = Epaisseur inf  5
        ///<para/>                      
        ///<para/>      List[2] (C3)    : ListCritere[0] (Critère 1) = Largeur inf  7
        /// </summary>
        public List<List<Criteria>> Criterias
        {
            get
            {
                return criterias;
            }

            set
            {
                criterias = value;
            }
        }

        /// <summary>
        /// Demande une instance pour creer (si aucun objet existe) ou accéder au singleton
        /// </summary>
        /// <returns>L'objet instancié </returns>
        public static Sorting Instance()
        {
            if (sorting == null)
            {
                sorting = new Sorting();
            }

            return sorting;
        }
    }
}

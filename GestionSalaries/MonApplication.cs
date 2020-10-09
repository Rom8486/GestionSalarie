using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalariesDll;
using Utilitaires;

namespace GestionSalaraies
{

  
  internal static class MonApplication
  {
      static Utilisateur _utilisateurConnecte;
      static ISauvegarde _dispositifSauvegarde = new SauvegardeXML();


        internal static ISauvegarde DispositifSauvegarde
      {
          get { return MonApplication._dispositifSauvegarde; }
      }

        internal static Utilisateur UtilisateurConnecte
      {
          get { return _utilisateurConnecte; }
          set { _utilisateurConnecte = value; }
      }


  }
}

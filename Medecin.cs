using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionMedicale
{
    internal class Medecin
    {
        public string Identifiant { get; set; }
        public string MotDePasse { get; set; }
        public string Nom { get; set; }
        public string Specialite { get; set; }

        public Medecin(string identifiant, string motDePasse, string nom, string specialite)
        {
            Identifiant = identifiant;
            MotDePasse = motDePasse;
            Nom = nom;
            Specialite = specialite;
        }
    }

}

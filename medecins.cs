using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionMedicale
{
    internal static class Medecins
    {
        public static List<Medecin> ListeMedecins { get; } = new List<Medecin>
        {
            new Medecin("drmartin", "pass123", "Dr. Martin", "Cardiologie"),
            new Medecin("drdupont", "med456", "Dr. Dupont", "Dermatologie"),
            new Medecin("drleblanc", "secure789", "Dr. Leblanc", "Pédiatrie"),
            new Medecin("drjean", "password", "Dr. Jean", "Orthopédie")
        };

        public static Medecin Authentifier(string identifiant, string motDePasse)
        {
            foreach (var medecin in ListeMedecins)
            {
                if (medecin.Identifiant == identifiant && medecin.MotDePasse == motDePasse)
                {
                    return medecin; // Authentification réussie
                }
            }
            return null; // Échec de l'authentification
        }
    }

}

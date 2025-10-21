using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using System.Collections.Generic;

namespace A_C.Infrastructure.Persistance
{
    public class DepotMedecins
    {
        private static readonly List<Medecin> _medecins = new List<Medecin>
        {
            new Medecin("drmartin", "pass123", "Dr. Martin", "Cardiologie") { Id = 1 },
            new Medecin("drdupont", "med456", "Dr. Dupont", "Dermatologie") { Id = 2 },
            new Medecin("drleblanc", "secure789", "Dr. Leblanc", "Pédiatrie") { Id = 3 },
            new Medecin("drjean", "password", "Dr. Jean", "Orthopédie") { Id = 4 }
        };

        public static List<Medecin> ObtenirTousMedecins() => _medecins;
    }
} 
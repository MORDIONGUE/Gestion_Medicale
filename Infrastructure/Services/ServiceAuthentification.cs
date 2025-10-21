using System.Linq;
using System.Threading.Tasks;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using A_C.Infrastructure.Persistance;

namespace A_C.Infrastructure.Services
{
    public class ServiceAuthentification : IServiceAuthentification
    {
        public async Task<Medecin> Authentifier(string identifiant, string motDePasse)
        {
            // Simulation d'une opération asynchrone
            return await Task.Run(() =>
            {
                var medecins = DepotMedecins.ObtenirTousMedecins();
                return medecins.FirstOrDefault(m => 
                    m.Identifiant == identifiant && 
                    m.MotDePasse == motDePasse);
            });
        }
    }
} 
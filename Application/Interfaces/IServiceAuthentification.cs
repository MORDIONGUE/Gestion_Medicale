using System.Threading.Tasks;
using A_C.Domaine.Entites;

namespace A_C.Application.Interfaces
{
    public interface IServiceAuthentification
    {
        Task<Medecin> Authentifier(string identifiant, string motDePasse);
    }
} 
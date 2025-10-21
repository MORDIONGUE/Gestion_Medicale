using System.Collections.Generic;
using System.Threading.Tasks;
using A_C.Domaine.Entites;

namespace A_C.Application.Interfaces
{
    public interface IDepot<T> where T : IEntite
    {
        Task<T> ObtenirParId(int id);
        Task<IEnumerable<T>> ObtenirTout();
        Task<T> Ajouter(T entite);
        Task<T> Modifier(T entite);
        Task Supprimer(int id);
    }
} 
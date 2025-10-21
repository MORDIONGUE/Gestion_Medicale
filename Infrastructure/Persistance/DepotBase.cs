using System.Collections.Generic;
using System.Threading.Tasks;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;

namespace A_C.Infrastructure.Persistance
{
    public abstract class DepotBase<T> : IDepot<T> where T : IEntite
    {
        public virtual async Task<T> Ajouter(T entite)
        {
            // Implémentation de base
            return await Task.FromResult(entite);
        }

        public virtual async Task<T> ObtenirParId(int id)
        {
            // Implémentation de base
            return await Task.FromResult<T>(default);
        }

        public virtual async Task<IEnumerable<T>> ObtenirTout()
        {
            // Implémentation de base
            return await Task.FromResult<IEnumerable<T>>(new List<T>());
        }

        public virtual async Task<T> Modifier(T entite)
        {
            // Implémentation de base
            return await Task.FromResult(entite);
        }

        public virtual async Task Supprimer(int id)
        {
            // Implémentation de base
            await Task.CompletedTask;
        }
    }
} 
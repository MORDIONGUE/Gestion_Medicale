using A_C.Domaine.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A_C.Application.Interfaces
{
    public interface IServiceDossierMedical
    {
        Task<DossierMedical> ObtenirDossierParId(int id);
        Task<DossierMedical> ObtenirDossierParPatient(int patientId);
        Task<DossierMedical> CreerDossier(DossierMedical dossier);
        Task<DossierMedical> ModifierDossier(DossierMedical dossier);
        Task<List<DossierMedical>> ObtenirTousLesDossiers();
        Task<bool> AjouterTraitement(int dossierId, string traitement);
    }
} 
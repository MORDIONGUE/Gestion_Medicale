using A_C.Domaine.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A_C.Application.Interfaces
{
    public interface IServicePrescription
    {
        Task<List<PrescriptionDetails>> ObtenirToutesLesPrescriptions();
        Task<List<PrescriptionDetails>> ObtenirPrescriptionsPatient(int patientId);
        Task<PrescriptionDetails> AjouterPrescription(int patientId, PrescriptionDetails prescription);
        Task<PrescriptionDetails> ModifierPrescription(PrescriptionDetails prescription);
        Task<PrescriptionDetails> ObtenirPrescriptionParId(int id);
        Task<bool> SupprimerPrescription(int id);
    }
} 
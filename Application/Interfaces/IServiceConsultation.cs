using System.Collections.Generic;
using System.Threading.Tasks;
using A_C.Domaine.Entites;

namespace A_C.Application.Interfaces
{
    public interface IServiceConsultation
    {
        Task<List<ConsultationDetails>> ObtenirToutesLesConsultations();
        Task<List<ConsultationDetails>> ObtenirConsultationsPatient(int patientId);
        Task<ConsultationDetails> AjouterConsultation(int patientId, ConsultationDetails consultation);
        Task<ConsultationDetails> ModifierConsultation(ConsultationDetails consultation);
        Task<ConsultationDetails> ObtenirConsultationParId(int id);
        Task<bool> SupprimerConsultation(int id);
    }
} 
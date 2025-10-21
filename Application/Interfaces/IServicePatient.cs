using A_C.Domaine.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace A_C.Application.Interfaces
{
    public interface IServicePatient
    {
        Task<List<Patient>> ObtenirTousLesPatients();
        Task<Patient> ObtenirPatientParId(int id);
        Task<bool> AjouterPatient(Patient patient);
        Task<bool> ModifierPatient(Patient patient);
        Task<bool> SupprimerPatient(int id);
    }
} 
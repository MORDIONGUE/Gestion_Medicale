using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A_C.Infrastructure.Services
{
    public class ServiceDossierMedical : IServiceDossierMedical
    {
        private static readonly List<DossierMedical> _dossiers = new List<DossierMedical>();
        private static int _nextId = 1;
        private readonly IServicePatient _servicePatient;

        public ServiceDossierMedical(IServicePatient servicePatient)
        {
            _servicePatient = servicePatient;
        }

        public async Task<DossierMedical> ObtenirDossierParId(int id)
        {
            return await Task.FromResult(_dossiers.FirstOrDefault(d => d.Id == id));
        }

        public async Task<DossierMedical> ObtenirDossierParPatient(int patientId)
        {
            var patient = await _servicePatient.ObtenirPatientParId(patientId);
            return await Task.FromResult(_dossiers.FirstOrDefault(d => d.Patient?.Id == patientId));
        }

        public async Task<DossierMedical> CreerDossier(DossierMedical dossier)
        {
            dossier.Id = _nextId++;
            _dossiers.Add(dossier);
            return await Task.FromResult(dossier);
        }

        public async Task<DossierMedical> ModifierDossier(DossierMedical dossier)
        {
            var existingDossier = _dossiers.FirstOrDefault(d => d.Id == dossier.Id);
            if (existingDossier != null)
            {
                existingDossier.Nom = dossier.Nom;
                existingDossier.Traitements = dossier.Traitements;
            }
            return await Task.FromResult(existingDossier);
        }

        public async Task<List<DossierMedical>> ObtenirTousLesDossiers()
        {
            return await Task.FromResult(_dossiers);
        }

        public async Task<bool> AjouterTraitement(int dossierId, string traitement)
        {
            var dossier = await ObtenirDossierParId(dossierId);
            if (dossier != null)
            {
                dossier.Traitements.Add(traitement);
                return true;
            }
            return false;
        }
    }
} 
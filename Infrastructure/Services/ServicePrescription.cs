using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;

namespace A_C.Infrastructure.Services
{
    public class ServicePrescription : IServicePrescription
    {
        private static readonly List<PrescriptionDetails> _prescriptions = new List<PrescriptionDetails>();
        private static int _nextId = 1;
        private readonly IServicePatient _servicePatient;

        public ServicePrescription(IServicePatient servicePatient)
        {
            _servicePatient = servicePatient;
        }

        public async Task<List<PrescriptionDetails>> ObtenirToutesLesPrescriptions()
        {
            return await Task.FromResult(_prescriptions);
        }

        public async Task<List<PrescriptionDetails>> ObtenirPrescriptionsPatient(int patientId)
        {
            return await Task.FromResult(_prescriptions.Where(p => p.PatientId == patientId).ToList());
        }

        public async Task<PrescriptionDetails> AjouterPrescription(int patientId, PrescriptionDetails prescription)
        {
            var patient = await _servicePatient.ObtenirPatientParId(patientId);
            if (patient == null)
                throw new ArgumentException("Patient non trouvé");

            prescription.Id = _nextId++;
            prescription.PatientId = patientId;
            prescription.Date = DateTime.Now;
            prescription.Etat = "En attente";

            _prescriptions.Add(prescription);
            patient.Prescriptions.Add(prescription);

            return await Task.FromResult(prescription);
        }

        public async Task<PrescriptionDetails> ModifierPrescription(PrescriptionDetails prescription)
        {
            var existingPrescription = _prescriptions.FirstOrDefault(p => p.Id == prescription.Id);
            if (existingPrescription == null)
                throw new ArgumentException("Prescription non trouvée");

            existingPrescription.Medicament = prescription.Medicament;
            existingPrescription.Dosage = prescription.Dosage;
            existingPrescription.Instructions = prescription.Instructions;
            existingPrescription.Duree = prescription.Duree;
            existingPrescription.Etat = prescription.Etat;

            return await Task.FromResult(existingPrescription);
        }

        public async Task<PrescriptionDetails> ObtenirPrescriptionParId(int id)
        {
            return await Task.FromResult(_prescriptions.FirstOrDefault(p => p.Id == id));
        }

        public async Task<bool> SupprimerPrescription(int id)
        {
            var prescription = _prescriptions.FirstOrDefault(p => p.Id == id);
            if (prescription == null)
                return false;

            _prescriptions.Remove(prescription);
            return await Task.FromResult(true);
        }
    }
} 
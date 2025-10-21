using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;
using A_C.Infrastructure.Services;

namespace A_C.Infrastructure.Services
{
    public class ServiceConsultation : IServiceConsultation
    {
        private static readonly List<ConsultationDetails> _consultations = new List<ConsultationDetails>();
        private static int _nextId = 1;
        private readonly IServicePatient _servicePatient;

        public ServiceConsultation(IServicePatient servicePatient)
        {
            _servicePatient = servicePatient;
        }

        public async Task<List<ConsultationDetails>> ObtenirToutesLesConsultations()
        {
            return await Task.FromResult(_consultations);
        }

        public async Task<List<ConsultationDetails>> ObtenirConsultationsPatient(int patientId)
        {
            return await Task.FromResult(_consultations.Where(c => c.PatientId == patientId).ToList());
        }

        public async Task<ConsultationDetails> AjouterConsultation(int patientId, ConsultationDetails consultation)
        {
            var patient = await _servicePatient.ObtenirPatientParId(patientId);
            if (patient == null)
                throw new ArgumentException("Patient non trouvé");

            consultation.Id = _nextId++;
            consultation.PatientId = patientId;
            consultation.Date = DateTime.Now;

            _consultations.Add(consultation);
            patient.Consultations.Add(consultation);

            return await Task.FromResult(consultation);
        }

        public async Task<ConsultationDetails> ModifierConsultation(ConsultationDetails consultation)
        {
            var existingConsultation = _consultations.FirstOrDefault(c => c.Id == consultation.Id);
            if (existingConsultation == null)
                throw new ArgumentException("Consultation non trouvée");

            existingConsultation.Motif = consultation.Motif;
            existingConsultation.Observation = consultation.Observation;
            existingConsultation.Diagnostic = consultation.Diagnostic;

            return await Task.FromResult(existingConsultation);
        }

        public async Task<ConsultationDetails> ObtenirConsultationParId(int id)
        {
            return await Task.FromResult(_consultations.FirstOrDefault(c => c.Id == id));
        }

        public async Task<bool> SupprimerConsultation(int id)
        {
            var consultation = _consultations.FirstOrDefault(c => c.Id == id);
            if (consultation == null)
                return false;

            _consultations.Remove(consultation);
            return await Task.FromResult(true);
        }
    }
} 
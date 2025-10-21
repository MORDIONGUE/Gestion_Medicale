using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using A_C.Application.Interfaces;
using A_C.Domaine.Entites;

namespace A_C.Infrastructure.Services
{
    public class ServicePatient : IServicePatient
    {
        private readonly List<Patient> _patients = new List<Patient>();

        public async Task<List<Patient>> ObtenirTousLesPatients()
        {
            return _patients;
        }

        public async Task<Patient> ObtenirPatientParId(int id)
        {
            return _patients.Find(p => p.Id == id);
        }

        public async Task<bool> AjouterPatient(Patient patient)
        {
            try
            {
                _patients.Add(patient);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ModifierPatient(Patient patient)
        {
            try
            {
                var index = _patients.FindIndex(p => p.Id == patient.Id);
                if (index != -1)
                {
                    _patients[index] = patient;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SupprimerPatient(int id)
        {
            try
            {
                var patient = await ObtenirPatientParId(id);
                if (patient != null)
                {
                    return _patients.Remove(patient);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
} 
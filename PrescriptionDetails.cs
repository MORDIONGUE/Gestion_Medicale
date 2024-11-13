using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionMedicale
{
    public class PrescriptionDetails
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Medicament { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public string Etat { get; set; } = "En attente"; // État par défaut
    }
}

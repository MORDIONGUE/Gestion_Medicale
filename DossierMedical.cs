using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionMedicale
{
    public class Dossier
    {
        public int Identifiant { get; set; }
        public string Nom { get; set; } = string.Empty;
        public List<string> Traitements { get; set; } = new();
    }
}


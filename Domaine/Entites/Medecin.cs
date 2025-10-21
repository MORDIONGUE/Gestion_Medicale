namespace A_C.Domaine.Entites
{
    public class Medecin : IEntite
    {
        public int Id { get; set; }
        public string Identifiant { get; set; }
        public string MotDePasse { get; set; }
        public string Nom { get; set; }
        public string Specialite { get; set; }

        public Medecin() { }

        public Medecin(string identifiant, string motDePasse, string nom, string specialite)
        {
            Identifiant = identifiant;
            MotDePasse = motDePasse;
            Nom = nom;
            Specialite = specialite;
        }
    }
} 
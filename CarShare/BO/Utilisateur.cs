using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public bool IsActif { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Promotion { get; set; }
        public Ecole Ecole { get; set; }
        public List<Emplacement> EmplacementsFavoris{ get; set; }
        public bool IsAdmin { get; set; }
        public virtual List<Trajet> ListeTrajets { get; set; }
        public virtual Conducteur Conducteur { get; set; }
    }
}

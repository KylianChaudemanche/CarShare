using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Trajet
    {
        public int Id { get; set; }
        public Adresse Depart { get; set; }
        public Adresse Arrive { get; set; }
        public DateTime Date { get; set; }
        public Conducteur Conducteur { get; set; }
        public List<Utilisateur> Passagers { get; set; }
        public List<Arret> Arrets { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Conducteur
    {
        public int Id { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public string Description { get; set; }
        public Voiture Voiture { get; set; }
        public virtual List<Trajet> ListeTrajets { get; set; }
    }
}

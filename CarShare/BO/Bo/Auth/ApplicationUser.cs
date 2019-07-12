using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public partial class  ApplicationUser
    {
        public bool IsActif { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public string Promotion { get; set; }
        public Ecole Ecole { get; set; }
        public List<Emplacement> EmplacementsFavoris { get; set; }
        public bool IsAdmin { get; set; }


        
    }
}

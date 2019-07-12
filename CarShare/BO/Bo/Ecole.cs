using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Ecole
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public Emplacement Coordonnees { get; set; }
    }
}

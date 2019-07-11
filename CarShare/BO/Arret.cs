using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    class Arret
    {
        public int Id { get; set; }
        public DateTime Horaire { get; set; }
        public Adresse Adresse { get; set; }
    }
}

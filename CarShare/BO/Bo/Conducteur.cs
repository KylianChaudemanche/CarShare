using CarShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Conducteur
    {
        public  ApplicationUser Utilisateur { get; set; }
        public string Description { get; set; }
        public Voiture Voiture { get; set; }
    }
}

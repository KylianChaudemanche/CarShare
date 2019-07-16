using CarShare.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShare.Models
{
    public class TrajetsViewModels
    {
        public Trajet Trajet { get; set; }
        public ApplicationUser Conducteur { get; set; }
        public List<ApplicationUser> Passagers { get; set; }
        public List<Arret> listeArrets { get; set; }
        public Emplacement Depart { get; set; }
        public Emplacement Arrivee { get; set; }
    }
}
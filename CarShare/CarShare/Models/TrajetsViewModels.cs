using CarShare.BO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public List<Emplacement> listeEmplacements { get; set; }
        public Emplacement Depart { get; set; }
        public Emplacement Arrivee { get; set; }
        [DataType(DataType.Time)]
        public DateTime selectedHeureDepart { get; set; }
        [DataType(DataType.Time)]
        public DateTime selectedHeureArrivee{ get; set; }
        [DataType(DataType.Date)]
        public DateTime selectedDateDebut { get; set; }
        [DataType(DataType.Date)]
        public DateTime selectedDateFin{ get; set; }
        [DataType(DataType.Date)]
        public DateTime selectedDate { get; set; }
        public int selectedDepart{ get; set; }
        public int selectedArrivee { get; set; }
    }
}
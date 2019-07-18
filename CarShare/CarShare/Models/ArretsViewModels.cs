using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using CarShare.BO;

namespace CarShare.Models
{
    public class ArretsViewModels
    {
        public Arret Arret { get; set; }

        public Trajet Trajet { get; set; }

        public Emplacement Emplacement { get; set; }

        [DataType(DataType.Time)]
        public DateTime Horaire { get; set; }

        [Display(Name = "Etat de l'arrêt")]
        [Range(1,3)]
        public int EtatArret { get; set; }


    }
}
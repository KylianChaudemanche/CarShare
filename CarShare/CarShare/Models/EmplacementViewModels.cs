using CarShare.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShare.Models
{
    public class EmplacementViewModels
    {
        public List<Emplacement> listeEmplacements { get; set; }
        public float selectedLatitude { get; set; }
        public float selectedLongitude { get; set; }
    }
}
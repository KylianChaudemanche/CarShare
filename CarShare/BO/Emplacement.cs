﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Emplacement
    {
        public int Id { get; set; }
        public string Intitule { get; set; }
        public string Description { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public virtual Ecole Ecole { get; set; }
        public virtual Arret Arret { get; set; }
    }
}

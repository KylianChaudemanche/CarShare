﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Conducteur:Utilisateur
    {
        public string Description { get; set; }
        public Voiture Voiture { get; set; }
    }
}

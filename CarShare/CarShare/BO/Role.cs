using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShare.BO
{
    public class Role:IdentityRole
    {
        public int Force { get; set; }
    }
}
using CarShare.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShare.Models
{
    public class ApplicationUsersVM
    {
        public ApplicationUser ApplicationUser { get; set; }

        public List<Role> ListRolesDispo { get; set; }

        public List<string> IdRolesSelected { get; set; }

        public List<Ecole> ListEcolesDispo { get; set; }

        public int IdEcoleSelected { get; set; }
    }
}
using CarShare.BO;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarShare.Models
{
    public class ListApplicationUsersVM
    {
        public List<ApplicationUser> ListApplicationUser { get; set; }

        public List<Role> ListRolesDispo { get; set; }
    }
}
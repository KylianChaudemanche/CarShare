using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.BO
{
    public class Ecole : IDbEntity
    {
        public string Nom { get; set; }
        public virtual Emplacement Emplacement { get; set; }
        public virtual List<ApplicationUser> ListeEleves { get; set; }
        
        private int id;

        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}

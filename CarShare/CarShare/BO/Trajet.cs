using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.BO
{
    public class Trajet : IDbEntity
    {
        public virtual Emplacement Depart { get; set; }
        public virtual Emplacement Arrive { get; set; }
        public DateTime Date { get; set; }
        public virtual ApplicationUser Conducteur { get; set; }
        public virtual List<ApplicationUser> Passagers { get; set; }
        public virtual List<Arret> Arrets { get; set; }

        private int id;

        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}

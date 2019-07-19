using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.BO
{
    public class trajet : IDbEntity
    {
        public Emplacement Depart { get; set; }
        public Emplacement Arrive { get; set; }
        public DateTime Date { get; set; }
        public ApplicationUser Conducteur { get; set; }
        public List<ApplicationUser> Passagers { get; set; }
        public List<Arret> Arrets { get; set; }

        private int id;

        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.BO
{
    public class Arret:IDbEntity
    {
        
        public DateTime Horaire { get; set; }
        public Emplacement Emplacement { get; set; }
        public virtual trajet Trajet { get; set; }
        public int EtatArret { get; set; }

        private int id;
        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}

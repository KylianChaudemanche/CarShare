using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.BO
{
    public class Voiture : IDbEntity
    {
        public int NbPlaces { get; set; }
        public string Immatriculation { get; set; }
        public string Couleur { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public virtual ApplicationUser Proprietaire { get; set; }

        private int id;

        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarShare.BO
{
    public class Conducteur : IDbEntity
    {
        public ApplicationUser Utilisateur { get; set; }
        public string Description { get; set; }
        public Voiture Voiture { get; set; }
        public virtual List<Trajet> ListeTrajets { get; set; }

        private int id;

        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}

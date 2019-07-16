using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.BO
{
    public class Emplacement : IDbEntity
    {
        public string Intitule { get; set; }
        public string Description { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        private int id;
        [Key]
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.BO
{
    public interface IDbEntity
    {
        [Key]
        int Id { get; set; }
    }
}

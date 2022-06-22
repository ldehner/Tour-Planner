using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Data
{
    public class Tours
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TourId { get; set; }
        public string Name { get; set; }
        [MaxLength(30)]
        public string Description { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
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
        [Key]
        public Guid TourId { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public string Description { get; set; } = String.Empty;
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public string Type { get; set; } = String.Empty;
        [ForeignKey("TourId")]
        public List<Logs> Logs { get; set; }
        [ForeignKey("TourIdStart")]
        public Adresses Start { get; set; }
        [ForeignKey("TourIdDest")]
        public Adresses Destination { get; set; }

    }

 
    public class Adresses
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdressId { get; set; }
        public Guid TourIdStart { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? Plz { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }


    public class Logs
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LogId { get; set; }
        [Required]
        public Guid TourId { get; set; }
        [Required]
        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public string Comment { get; set; } = String.Empty;
        [Required]
        public Int16 Difficulty { get; set; }
        [Required]
        public Int16 Rating { get; set; }
    }


}

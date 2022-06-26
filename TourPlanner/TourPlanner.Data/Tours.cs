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
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        [MaxLength(30)]
        public string Description { get; set; } = String.Empty;
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public double Distance { get; set; }
        [Required]
        public string Type { get; set; } = String.Empty;
        [Required]
        public string Start { get; set; } = String.Empty;
        [Required]
        public string Destination { get; set; } = String.Empty;
        [ForeignKey("TourId")]
        public List<Logs> Logs { get; set; }

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

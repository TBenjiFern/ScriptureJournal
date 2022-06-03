using System;
using System.ComponentModel.DataAnnotations;

namespace ScriptureJournal.Models
{
    public class Scripture
    {
        public int ScriptureId { get; set; }
        
        [StringLength(60, MinimumLength = 2)]
        [Required]
        public string Book { get; set; } = string.Empty;
        
        [RegularExpression(@"^[0-9]+[0-9""'\s-]*$")]
        [StringLength(10)]
        [Required]
        public string Chapter { get; set; } = string.Empty;

        [RegularExpression(@"^[0-9]+[0-9""'\s-]*$")]
        [StringLength(30)]
        [Required]
        public string Verse { get; set; } = string.Empty;
        
        public string Notes { get; set; } = string.Empty;
        
        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        [Required]
        public DateTime DateAdded { get; set; }
    }
}

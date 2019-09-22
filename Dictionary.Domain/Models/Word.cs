using System;
using System.ComponentModel.DataAnnotations;

namespace Dictionary.Domain.Models
{
    public class Word
    {
        [Key]
        public Guid WordId { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Title} - {Description}";
        }
    }
}

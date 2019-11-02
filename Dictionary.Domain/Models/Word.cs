using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dictionary.Domain.Models.Abstract;

namespace Dictionary.Domain.Models
{
    [Table("Words")]
    public class Word : IEntity
    {
        [Key]
        [Required]
        [Column("WordId")]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Word is required.")]
        public string Title { get; set; }

        public ICollection<FullWord> WordDescriptions { get; set; }

        public Word()
        {
            WordDescriptions = new List<FullWord>();
        }
    }
}

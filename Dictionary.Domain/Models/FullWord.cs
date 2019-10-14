using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dictionary.Domain.Models.Abstract;

namespace Dictionary.Domain.Models
{
    [Table("WordsDescriptions")]
    public class FullWord : IEntity
    {
        [Key]
        [Required]
        [Column("WordDescriptionId")]
        public Guid Id { get; set; }

        public Guid WordId { get; set; }

        public Word Word { get; set; }

        public Guid DescriptionId { get; set; }

        public Description Description { get; set; }
    }
}

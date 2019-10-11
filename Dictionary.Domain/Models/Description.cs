using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dictionary.Domain.Models.Abstract;

namespace Dictionary.Domain.Models
{
    [Table("Descriptions")]
    public class Description : IEntity
    {
        [Key]
        [Required]
        [Column("DescriptionId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Content { get; set; }

        [ForeignKey(nameof(WordId))]
        public Guid WordId { get; set; }

        public Word Word { get; set; }
    }
}

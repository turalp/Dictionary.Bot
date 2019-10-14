using System;
using System.Collections.Generic;
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

        public ICollection<FullWord> WordDescriptions { get; set; }

        public Description()
        {
            WordDescriptions = new List<FullWord>();
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dictionary.Domain.Models.Abstract;

namespace Dictionary.Domain.Models
{
    [Table("Logs")]
    public class Log : IEntity
    {
        [Key]
        [Required]
        [Column("LogId")]
        public Guid Id { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public DateTime ThrownOn { get; set; }
    }
}

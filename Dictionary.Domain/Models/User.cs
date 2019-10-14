using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dictionary.Domain.Models.Abstract;
using Telegram.Bot.Types;

namespace Dictionary.Domain.Models
{
    [Table("Users")]
    public class User : Contact, IEntity
    {
        [Key]
        [Required]
        [Column("TelegramUserId")]
        public Guid Id { get; set; }
    }
}

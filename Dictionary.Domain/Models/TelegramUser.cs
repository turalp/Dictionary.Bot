using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dictionary.Domain.Models.Abstract;
using Telegram.Bot.Types;

namespace Dictionary.Domain.Models
{
    [Table("Users")]
    public class TelegramUser : User, IEntity
    {
        [Key]
        [Required]
        [Column("TelegramUserId")]
        public new Guid Id { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}

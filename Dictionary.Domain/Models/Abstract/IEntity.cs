using System;

namespace Dictionary.Domain.Models.Abstract
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}

using Flunt.Notifications;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace el.api.locathales.Domain.Core.Entities
{
    public abstract class Entity : Notifiable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; private set; }
    }
}

using Flunt.Notifications;

namespace el.api.locathales.Domain.Core.ValueObjects
{
    public abstract class ValueObject : Notifiable
    {
        public ValueObject GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }
    }
}

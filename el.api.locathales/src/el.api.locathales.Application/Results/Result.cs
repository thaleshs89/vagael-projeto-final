using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace el.api.locathales.Application
{
    public class Result : Notifiable
    {
        public bool Success { get { return !Notifications.Any(); } }

        protected Result()
        {
        }

        protected Result(IReadOnlyCollection<Notification> notifications)
        {
            AddNotifications(notifications);
        }

        public static Result Ok()
        {
            return new Result();
        }

        public static Result Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result(notifications);
        }
        public static Result Error(string msgErro, string propriedade)
        {
            return new Result(new List<Notification>(){
                    new Notification(propriedade, msgErro ) });
        }
    }

    public class Result<T> : Notifiable where T : class
    {
        public bool Success { get { return !Notifications.Any(); } }
        public T Object { get; }

        private Result(T obj)
        {
            Object = obj;
        }

        private Result(IReadOnlyCollection<Notification> notifications)
        {
            Object = null;
            AddNotifications(notifications);
        }

        public static Result<T> Ok(T obj)
        {
            return new Result<T>(obj);
        }

        public static Result<T> Error(IReadOnlyCollection<Notification> notifications)
        {
            return new Result<T>(notifications);
        }
        public static Result<T> Error(string msgErro, string propriedade )
        {
            return new Result<T>(new List<Notification>(){
                    new Notification(propriedade, msgErro ) });
        }

    }
}
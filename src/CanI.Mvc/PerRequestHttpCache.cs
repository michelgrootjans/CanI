using System.Web;
using CanI.Core.Configuration;

namespace CanI.Mvc
{
    public class PerRequestHttpCache : ICache
    {
        public T Get<T>()
        {
            return (T) HttpContext.Current.Items[typeof (T).FullName];
        }

        public void Store<T>(T item)
        {
            HttpContext.Current.Items[typeof (T).FullName] = item;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace CanI.Demo.Domain
{
    public interface IRepository
    {
        IEnumerable<T> FindAll<T>();
        T FindOne<T>(Func<T, bool> predicate);
    }

    public class StaticInMemoryRepository : IRepository
    {
        private static readonly IDictionary<Type, object> Database;

        static StaticInMemoryRepository()
        {
            Database = new Dictionary<Type, object>();

            Database[typeof (Customer)] = new List<Customer>
            {
                new Customer("Apple"),
                new Customer("Microsoft") {CanDelete = false},
                new Customer("Amazon"),
                new Customer("Dropbox")
            };
        }

        public IEnumerable<T> FindAll<T>()
        {
            return GetListOf<T>();
        }

        public T FindOne<T>(Func<T, bool> predicate)
        {
            var objects = GetListOf<T>();
            return objects == null ? default(T) : objects.FirstOrDefault(predicate);
        }

        public void Add<T>(T t)
        {
            GetListOf<T>().Add(t);
        }

        public void Remove<T>(Func<T, bool> predicate)
        {
            var list = GetListOf<T>();
            var element = list.First(predicate);
            list.Remove(element);
        }

        private static IList<T> GetListOf<T>()
        {
            return Database[typeof(T)] as IList<T> ?? new List<T>();
        }
    }
}
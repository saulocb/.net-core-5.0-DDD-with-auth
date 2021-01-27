using System.Linq;

namespace Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return source.Skip(page * pageSize).Take(pageSize);
        }
    }
}

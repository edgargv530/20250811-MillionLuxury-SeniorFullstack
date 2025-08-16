using MillionLuxury.TechhicalTest.Domain.Extensions;
using MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions;

namespace MillionLuxury.TechhicalTest.ApiRest.Extensions
{
    public static class QueryCollectionExtension
    {
        public static QueryRequest GetODataRequest(this IQueryCollection source)
        {
            var queryRequest = new QueryRequest();

            if (!source.Any())
            {
                return queryRequest;
            }

            queryRequest.Top = CastTop(source);
            queryRequest.Skip = CastSkip(source);
            queryRequest.Filters = CastFilter(source);
            queryRequest.OrdersBy = CastOrderBy(source);

            return queryRequest;
        }

        private static int? CastTop(IQueryCollection source)
        {
            if (source.TryGetValue("$top", out var top) && !string.IsNullOrWhiteSpace(top))
            {
                return top.First()!.CastInt();
            }
            return null;
        }

        private static int? CastSkip(IQueryCollection source)
        {
            if (source.TryGetValue("$skip", out var skip) && !string.IsNullOrWhiteSpace(skip))
            {
                return skip.First()!.CastInt();
            }
            return null;
        }

        private static IEnumerable<IQueryFilter> CastFilter(IQueryCollection source)
        {
            if (source.TryGetValue("$filter", out var filter) && !string.IsNullOrWhiteSpace(filter))
            {
                return filter.First()!.CastQueryFilters();
            }
            return null!;
        }

        private static IEnumerable<QueryOrderBy> CastOrderBy(IQueryCollection source)
        {
            if (source.TryGetValue("$orderby", out var orderby) && !string.IsNullOrWhiteSpace(orderby))
            {
                return orderby.First()!.CastQueryOrderBy();
            }
            return null!;
        }
    }
}


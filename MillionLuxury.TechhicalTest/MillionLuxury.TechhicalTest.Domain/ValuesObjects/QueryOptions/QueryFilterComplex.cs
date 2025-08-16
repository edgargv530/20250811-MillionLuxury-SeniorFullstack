using MillionLuxury.TechhicalTest.Domain.Enums;

namespace MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions
{
    public class QueryFilterComplex : IQueryFilter
    {
        public FilterPredicateType PredicateType { get; set; }

        public IEnumerable<IQueryFilter> Filters { get; set; } = null!;
    }
}

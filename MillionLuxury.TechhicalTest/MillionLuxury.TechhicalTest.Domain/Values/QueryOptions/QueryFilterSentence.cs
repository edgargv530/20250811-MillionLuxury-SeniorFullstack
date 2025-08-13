using MillionLuxury.TechhicalTest.Domain.Enums;

namespace MillionLuxury.TechhicalTest.Domain.Values.QueryOptions
{
    public class QueryFilterSentence : IQueryFilter
    {
        public string FieldName { get; set; } = null!;

        public FilterOperator Operator { get; set; }

        public dynamic? Value { get; set; }
    }
}

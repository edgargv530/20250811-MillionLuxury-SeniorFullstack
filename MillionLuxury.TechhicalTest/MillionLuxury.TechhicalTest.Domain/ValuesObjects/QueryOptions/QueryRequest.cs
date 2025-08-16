namespace MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions
{
    public class QueryRequest
    {
        private int? _top;
        private int? _skip;

        public int? Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value.HasValue ? (value < 0 ? 10 : (value > 100 ? 100 : value)) : 10;
            }
        }

        public int? Skip
        {
            get
            {
                return _skip;
            }
            set
            {
                _skip = value.HasValue && value < 0 ? 0 : value;
            }
        }

        public IEnumerable<QueryOrderBy> OrdersBy { get; set; } = null!;

        public IEnumerable<IQueryFilter> Filters { get; set; } = null!;
    }
}

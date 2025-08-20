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
                _top = value.HasValue ? (value < 0 ? 10 : (value > 100 ? 100 : value)) : 100;
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
                _skip = value.HasValue ? (value < 0 ? 0 : value) : 0;
            }
        }

        public IEnumerable<QueryOrderBy> OrdersBy { get; set; } = null!;

        public IEnumerable<IQueryFilter> Filters { get; set; } = null!;
    }
}

namespace MillionLuxury.TechhicalTest.Domain.ValuesObjects.QueryOptions
{
    public class QueryResponse<T> where T : class
    {
        public int Top { get; set; }
        public int Skip { get; set; }
        public long TotalRows { get; set; }

        public IEnumerable<T> Data { get; set; } = null!;
    }
}

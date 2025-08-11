using MillionLuxury.TechhicalTest.Domain.Enums;

namespace MillionLuxury.TechhicalTest.Domain.Values
{
    public class Error
    {
        public string Message { get; set; } = null!;

        public IList<string> Errors { get; set; } = null!;

        public ErrorType ErrorType { get; set; } = ErrorType.None;
    }
}

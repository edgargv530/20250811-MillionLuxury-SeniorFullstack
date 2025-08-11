namespace MillionLuxury.TechhicalTest.Application.Models.Owners
{
    public class OwnerModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public byte[] Photo { get; set; } = null!;

        public DateOnly Birthday { get; set; }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MillionLuxury.TechhicalTest.Infraestructure.Data.DataModels
{
    internal class OwnerDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        //public byte[] Photo { get; set; } = null!;

        //public DateOnly Birthday { get; set; }
    }
}

using MongoDB.Bson;

namespace HepsiYemek.Core.Entities
{
    public abstract class DocumentDbEntity
    {
        public ObjectId Id { get; set; }
        public string ObjectId => Id.ToString();
    }
}

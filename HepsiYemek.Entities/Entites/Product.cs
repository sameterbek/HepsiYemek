using HepsiYemek.Core.Entities;
using MongoDB.Bson;

namespace HepsiYemek.Entities.Entites
{
    public class Product : DocumentDbEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public ObjectId categoryId { get; set; }
        public decimal? price { get; set; }
        public string currency { get; set; }
    }
}

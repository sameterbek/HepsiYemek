using HepsiYemek.Core.Entities;

namespace HepsiYemek.Entities.Entites
{
    public class Category : DocumentDbEntity
    {
        public string name { get; set; }
        public string description { get; set; }
    }
}

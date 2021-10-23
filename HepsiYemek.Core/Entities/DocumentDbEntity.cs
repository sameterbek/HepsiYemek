using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.Core.Entities
{
    public abstract class DocumentDbEntity
    {
        public ObjectId Id { get; set; }
        public string ObjectId => Id.ToString();
    }
}

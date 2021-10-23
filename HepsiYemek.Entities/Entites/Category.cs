using HepsiYemek.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.Entities.Entites
{
    public class Category : DocumentDbEntity
    {
        public string name { get; set; }
        public string description { get; set; }
    }
}

using HepsiYemek.Core.DataAccess;
using HepsiYemek.Entities.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.DataAccess.Abstract
{
    public interface ICategoryRepository : IDocumentDbRepository<Category>
    {
    }
}

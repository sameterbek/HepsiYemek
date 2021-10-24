using HepsiYemek.Core.DataAccess;
using HepsiYemek.Entities.Entites;

namespace HepsiYemek.DataAccess.Abstract
{
    public interface ICategoryRepository : IDocumentDbRepository<Category>
    {
    }
}

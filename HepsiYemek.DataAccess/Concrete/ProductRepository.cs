using HepsiYemek.Core.DataAccess.MongoDb;
using HepsiYemek.DataAccess.Abstract;
using HepsiYemek.DataAccess.Concrete.Context;
using HepsiYemek.Entities.Entites;

namespace HepsiYemek.DataAccess.Concrete
{
    public class ProductRepository : MongoDbRepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}

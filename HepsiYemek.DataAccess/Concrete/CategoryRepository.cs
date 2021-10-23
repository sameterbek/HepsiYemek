using HepsiYemek.Core.DataAccess.MongoDb;
using HepsiYemek.Core.DataAccess.MongoDb.Configurations;
using HepsiYemek.DataAccess.Abstract;
using HepsiYemek.DataAccess.Concrete.Context;
using HepsiYemek.Entities.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.DataAccess.Concrete
{
    public class CategoryRepository : MongoDbRepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(MongoDbContextBase mongoDbContext, string collectionName) : base(mongoDbContext.MongoConnectionSettings, collectionName)
        {
        }
    }
}

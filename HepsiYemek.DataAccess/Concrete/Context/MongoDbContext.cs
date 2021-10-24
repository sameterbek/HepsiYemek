using Microsoft.Extensions.Configuration;

namespace HepsiYemek.DataAccess.Concrete.Context
{
    public class MongoDbContext : MongoDbContextBase
    {
        public MongoDbContext(IConfiguration configuration)
            : base(configuration)
        {

        }

    }
}

using HepsiYemek.Core.DataAccess.MongoDb.Configurations;
using Microsoft.Extensions.Configuration;

namespace HepsiYemek.DataAccess.Concrete.Context
{
    public abstract class MongoDbContextBase
    {
        protected MongoDbContextBase(IConfiguration configuration)
        {
            Configuration = configuration;
            MongoConnectionSettings = configuration.GetSection("MongoDbSettings").Get<MongoConnectionSettings>();
        }

        public IConfiguration Configuration { get; }
        public MongoConnectionSettings MongoConnectionSettings { get; }
    }
}

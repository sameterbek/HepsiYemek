using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

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

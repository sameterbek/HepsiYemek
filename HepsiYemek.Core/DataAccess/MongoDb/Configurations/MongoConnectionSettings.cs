﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HepsiYemek.Core.DataAccess.MongoDb.Configurations
{
    public class MongoConnectionSettings
    {
        public MongoConnectionSettings()
        {

        }

        public MongoConnectionSettings(MongoClientSettings mongoClientSettings)
        {
            MongoClientSettings = mongoClientSettings;
        }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        /// <summary>
        /// To be set if the MongoClientSetting class is to be used.
        /// </summary>
        private MongoClientSettings MongoClientSettings { get; set; }

        public MongoClientSettings GetMongoClientSettings()
        {
            return MongoClientSettings;
        }
    }
}
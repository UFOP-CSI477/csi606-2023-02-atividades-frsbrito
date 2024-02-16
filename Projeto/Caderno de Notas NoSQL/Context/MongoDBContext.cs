using System;
using Caderno_de_Notas_NoSQL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Caderno_de_Notas_NoSQL.Context
{
    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; }
        private readonly IOptions<MongoDbSettings> _options;

        public MongoDBContext(IOptions<MongoDbSettings> options)
        {
            try
            {
                _options = options;
                var client = new MongoClient(_options.Value.ConnectionString);
                _db = client.GetDatabase(_options.Value.DatabaseName);
                _db.CreateCollection(_options.Value.CollectionName);
            }
            catch (Exception ex)
            {
                throw new MongoException("Não foi possível se conectar ao servidor MongoDB", ex);
            }
        }
        public IMongoCollection<Nota> Notas => _db.GetCollection<Nota>(_options.Value.CollectionName);

    }
}


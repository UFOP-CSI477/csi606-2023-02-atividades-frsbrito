using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Caderno_de_Notas_NoSQL.Models
{
    [BsonIgnoreExtraElements]
    public class Nota
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Nome { get; set; }
        
        public string? Anotacoes { get; set; }

        [JsonIgnore]
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public DateTime LastModifiedUtc { get; set; } = DateTime.UtcNow;
    }
}


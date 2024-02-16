using System;
using Caderno_de_Notas_NoSQL.Models;
using MongoDB.Driver;

namespace Caderno_de_Notas_NoSQL.Context
{
	public interface IMongoDBContext
	{
		IMongoCollection<Nota> Notas { get; }
	}
}


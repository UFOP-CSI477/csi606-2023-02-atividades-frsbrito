using Caderno_de_Notas_NoSQL.Context;
using Caderno_de_Notas_NoSQL.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Caderno_de_Notas_NoSQL.Repository
{
    public class NotasRepository : INotasRepository
    {
        private readonly IMongoDBContext _context;
        private readonly ILogger<NotasRepository> logger;

        public NotasRepository(IMongoDBContext context, ILogger<NotasRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }

        public Task<Nota> Get(string id)
        {
            try
            {
                logger.LogInformation("Buscando nota por id: {0}", id);
                var filter = Builders<Nota>.Filter.Eq(t => t.Id, id);

                return _context.Notas.Find(filter: filter).FirstOrDefaultAsync();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Erro ao buscar nota por id: {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Nota>> ListarAsync(string query)
        {
            try
            {
                string[] palavras = query.Split(' ');
                var filter = Builders<Nota>.Filter.Empty;
                foreach (var palavra in palavras)
                {
                    filter = filter & Builders<Nota>.Filter.Regex(t => t.Nome, new BsonRegularExpression(palavra, "i"));
                }
                var notas = await _context
                    .Notas.Find(filter: filter)
                    .ToListAsync();

                return notas
                    .OrderBy(t => t.CreatedUtc);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao listar notas: {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Nota>> GetAll()
        {
            try
            {
                var values = await _context
                    .Notas
                    .Find(_ => true)
                    .ToListAsync();

                return values
                    .OrderBy(t => t.CreatedUtc);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao listar todas as notas: {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> InserirNota(Nota value)
        {
            try
            {
                await _context.Notas.InsertOneAsync(value);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao inserir nota: {0}", ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(Nota value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Id))
                    throw new ArgumentOutOfRangeException(nameof(value));

                var filter = Builders<Nota>.Filter.Eq(t => t.Id, value.Id);

                var updateDefinition =
                    Builders<Nota>.Update
                        .Set(t => t.Nome, value.Nome)
                        .Set(t => t.Nome, value.Anotacoes)
                        .Set(t => t.LastModifiedUtc, DateTime.UtcNow);

                var updateResult =  
                    await _context
                            .Notas
                            .UpdateOneAsync(
                                filter: filter,
                                update: updateDefinition);

                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch(Exception ex)
            {
                logger.LogError("Erro ao atualizar nota: {0}", value.Id);
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(Nota value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.Id))
                    throw new ArgumentOutOfRangeException(nameof(value));

                var filter = Builders<Nota>.Filter.Eq(t => t.Id, value.Id);

                var deleteResult =
                    await _context
                            .Notas
                            .DeleteOneAsync(filter: filter);

                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch(Exception ex)
            {
                logger.LogError("Erro ao deletar nota: {0}", value.Id);
                throw new Exception(ex.Message);
            }
        }
    }
}


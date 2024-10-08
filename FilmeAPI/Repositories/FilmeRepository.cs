using MongoDB.Driver;
using Models;
using Data;
using MongoDB.Bson;

namespace Repositories
{
    public class FilmeRepository : BaseRepository<FilmeModel>, IFilmeRepository
    {
        private readonly IMongoCollection<FilmeModel> _filme; 
        public FilmeRepository(MongoDbContext context) : base(context.Filmes)
        {
            _filme = context.Filmes; 
        }


        public async Task<IEnumerable<FilmeModel>> GetFilmesByYearAsync(int Year)
        {
            return await _filme.Find(filme => filme.AnoLancamento == Year).ToListAsync();
        }
        public async Task<IEnumerable<FilmeModel>> GetFilmesByTitleAsync(string Title)
        {
            var filter = Builders<FilmeModel>.Filter.Regex(filme => filme.Titulo, new BsonRegularExpression(Title, "i"));
            return await _filme.Find(filter).ToListAsync(); 
        }
    }
}

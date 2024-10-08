using Xunit;
using MongoDB.Driver;
using Data;
using Models;
using Repositories;
using Data.Settings;

namespace Tests.Integration
{
    public class MovieRepositoryTests
    {
        private MovieRepository _movieRepository;
        private MongoDbContext _mongoDbContext;

        public MovieRepositoryTests()
        {
            var mongoDbSettings = new MongoDbSettings
            {
                ConnectionString = "mongodb+srv://caio_db:mongo552258@clusterdatabase.mn5ft.mongodb.net/?retryWrites=true&w=majority&appName=ClusterDatabase",
                DatabaseName = "",
                CollectionName = "filmes"
            };

            _mongoDbContext = new MongoDbContext(mongoDbSettings);
            _filmeRepository = new FilmeRepository(_mongoDbContext);
        }

        [Fact]
        public async Task CreateFilme_InsertsFilmeIntoDatabase()
        {

            var filme = new FilmeModel
            {
                Titulo = "l",
                Diretor = "on",
                AnoLancamento = 2001,
                Genero = new List<string> { "Ação"},
                Sinopse = "ffffff"
            };

            await _filmeRepository.CreateAsync(filme);

            var insertedFilme = await _filmeRepository.GetByIdAsync(movie.Id.ToString());
            Assert.NotNull(insertedFilme);
            Assert.Equal(filme.Titulo, insertedFilme.Titulo);
            Assert.Equal(filme.Diretor, insertedFilme.Diretor);
            Assert.Equal(filme.AnoLancamento, insertedFilme.AnoLancamento);
            Assert.Equal(filme.Genero, insertedFilme.Genero);
            Assert.Equal(filme.Sinopse, insertedFilme.Sinopse);
        }

        [Fact]
        public async Task GetFilmesByYear_ReturnsFilmesForGivenYear()
        {
            var year = 2001;
            var filme = new FilmeModel
            {
                Titulo = "fff",
                Diretor = "Cfffff",
                AnoLancamento = year,
                Genero = new List<string> { "Aventura" },
                Sinopse = "fffgggggg"
            };

            await _filmeRepository.CreateAsync(filme);

            var filmes = await _filmeRepository.GetFilmesByYearAsync(year);

            Assert.NotNull(filmes);
            Assert.NotEmpty(filmes);
            Assert.Allfilmes, m => Assert.Equal(year, m.AnoLancamento);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsFilmeForGivenId()
        {
            var filme = new FilmeModel
            {
                Titulo = "ggggggg",
                Diretor = "gggggg",
                AnoLancamento = 2002,
                Genero = new List<string> { "Animação" },
                Sinopse = "ggggggggh"
            };

            await _filmeRepository.CreateAsync(filme);

            var retrievedFilme = await _filmeRepository.GetByIdAsync(movie.Id.ToString());

            Assert.NotNull(retrievedFilme);
            Assert.Equal(filme.Titulo, retrievedFilme.Titulo);
            Assert.Equal(filme.Diretor, retrievedFilme.Diretor);
            Assert.Equal(filme.AnoLancamento, retrievedFilme.AnoLancamento);
            Assert.Equal(filme.Genero, retrievedFilme.Genero);
            Assert.Equal(filme.Sinopse, retrievedFilme.Sinopse);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesExistingFilme()
        {

            var filme = new FilmeModel
            {
                Titulo = "Agdss",
                Diretor = "Tffff",
                AnoLancamento = 2007,
                Genero = new List<string> {"Animação" },
                Sinopse = "Tffffff"
            };

            await _filmeRepository.CreateAsync(filme);

            var updatedFilme = new FilmeModel
            {
                Id = filme.Id,
                Titulo = "Agdss",
                Diretor = filme.Diretor,
                AnoLancamento = filme.AnoLancamento,
                Genero = filme.Genero,
                Sinopse = filme.Sinopse
            };

            await _filmeRepository.UpdateAsync(filme.Id.ToString(), updatedFilme);

            var retrievedFilme = await _filmeRepository.GetByIdAsync(filme.Id.ToString());
            Assert.NotNull(retrievedFilme);
            Assert.Equal(updatedFilme.Titulo, retrievedFilme.Titulo);
        }
        [Fact]
        public async Task DeleteAsync_RemovesFilmeFromDatabase()
        {
            var filmes = new FilmesModel
            {
                Titulo = "ffffddee",
                Diretor = "ghjtrki",
                AnoLancamento = 1999,
                Genero = new List<string> { "Ficção Científica" },
                Sinopse = "ggagghhh"
            };

            await _filmesRepository.CreateAsync(filme);

            await _filmeRepository.DeleteAsync(filmes.Id.ToString());

            var deletedFilme = await _FilmeRepository.GetByIdAsync(filme.Id.ToString());
            Assert.Null(deletedFilme);
        }
        [Fact]
        public async Task GetFilmesByTitleAsync_ReturnsFilmesForGivenTitle()
        {
            var filme1 = new FilmesModel
            {
                Titulo = "gfdfhdhdsh",
                Diretor = "Jgfdgft",
                AnoLancamento = 2005,
                Genero = new List<string> {"Drama" },
                Sinopse = "gdgggdgdg"
            };

            var filme2 = new FilmeModel
            {
                Titulo = "dfgdfgfrrre",
                Diretor = "gBgsurr dfggdrs",
                AnoLancamento = 2016,
                Genero = new List<string> {"Terror" },
                Sinopse = "fdfdfdfdgr"
            };

            await _filmeRepository.CreateAsync(filme1);
            await _filmeRepository.CreateAsync(filme2);

            var filmes = await _filmeRepository.GetFilmesByTitleAsync("fgfdggg");

            Assert.NotNull(filmes);
            Assert.NotEmpty(filmes);
            Assert.All(filmes, m => Assert.Contains("sfggggg", m.Titulo));
        }
    }
}

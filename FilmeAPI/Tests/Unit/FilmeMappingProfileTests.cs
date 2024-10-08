using AutoMapper;
using DTOs;
using Mapping;
using Models;
using MongoDB.Bson;
using Xunit;

namespace Tests.Unit
{

    public class FilmeMappingProfileTests
    {
        private readonly IMapper _mapper;

        public FilmeMappingProfileTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FilmeMappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ShouldMap_FilmeRequestDTO_ToFilmeModel()
        {
            var filmeRequest = new FilmeRequestDTO
            {
                Titulo = "dddddddddd",
                Diretor = "Dddddddddddddddd",
                AnoLancamento = 2022,
                Genero = new List<string> { "Ação", "Suspense" },
                Sinopse = " ddddddddddddddd."
            };

            var filmeModel = _mapper.Map<MovieModel>(filmeRequest);

            Assert.NotNull(filmeModel);
            Assert.Equal(filmeRequest.Titulo, filmeModel.Titulo);
            Assert.Equal(filmeRequest.Diretor, filmeModel.Diretor);
            Assert.Equal(filmeRequest.AnoLancamento, filmeModel.AnoLancamento);
            Assert.Equal(filmeRequest.Genero, filmeModel.Genero);
            Assert.Equal(filmeRequest.Sinopse, filmeModel.Sinopse);
        }

        [Fact]
        public void ShouldMap_FilmeModel_ToFilmeResponseDTO()
        {

            var filmeModel = new FilmeModel
            {
                Id = ObjectId.GenerateNewId(), 
                Titulo = "dddddddddddddddddddase",
                Diretor = "ffffffffffffffffff",
                AnoLancamento = 2023,
                Genero = new List<string> { "Ação", "Ficção Científica" },
                Sinopse = "fffffffffffff."
            };

            var filmeResponse = _mapper.Map<FilmeResponseDTO>(filmeModel);

            Assert.NotNull(filmeResponse);
            Assert.Equal(filmeModel.Titulo, filmeResponse.Titulo);
            Assert.Equal(filmeModel.Diretor, filmeResponse.Diretor);
            Assert.Equal(filmeModel.AnoLancamento, filmeResponse.AnoLancamento);
            Assert.Equal(filmeModel.Genero, filmeResponse.Genero);
            Assert.Equal(filmeModel.Sinopse, filmeResponse.Sinopse);
            Assert.Equal(filmeModel.Id.ToString(), filmeResponse.Id); 
        }
    }
}

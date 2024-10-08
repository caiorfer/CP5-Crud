using AutoMapper;
using DTOs;
using Newtonsoft.Json;
using System.Text;
using Mapping;
using System.Net;

namespace Tests.Integration
{
    public class FilmesControllerTests
    {
        private readonly HttpClient _httpClient;

        public FilmesControllerTests()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5072/api/")
            };
        }

        [Fact]
        public async Task CreateFilme_ShouldReturnBadRequest_WhenDirectorIsNumber()
        {
            var invalidFilmes = new
            {
                Titulo = "fggafff",
                Diretor = 444444,
                AnoLancamento = 1993,
                Genero = new List<string> { "Ação", "Aventura" },
                Sinopse = "fffffffff"
            };

            var jsonContent = JsonConvert.SerializeObject(invalidFilme);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("filmes", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateFilme_ShouldReturnBadRequest_WhenFilmesRequestIsEmpty()
        {

            var emptyFilme = new FilmeRequestDTO(); 
            var jsonContent = JsonConvert.SerializeObject(emptyFilme);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("filmes", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateFilme_ShouldReturnCreated_WhenFilmeIsValid()
        {
            var validFilme = new FilmeRequestDTO
            {
                Titulo = "ddddddddd-d",
                Diretor = "essee eeeee",
                AnoLancamento = 2010,
                Genero = new List<string> { "Cringe" },
                Sinopse = "ddddddddddddddddddd"
            };

            var jsonContent = JsonConvert.SerializeObject(validFilme);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("filmes", content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetFilmesByYear_ShouldReturnNotFound_WhenNoFilmesFoundForYear()
        {
            var nonexistentYear = 3000; 

            var response = await _httpClient.GetAsync($"filmes/year/{nonexistentYear}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdateFilme_ShouldReturnOk_WhenFilmeIsUpdatedSuccessfully()
        {

            var validFilme = new FilmeRequestDTO
            {
                Titulo = "ssssssss",
                Diretor = "Jgggggg",
                AnoLancamento = 1900,
                Genero = new List<string> { "Ação"},
                Sinopse = "idk"
            };

            var jsonContent = JsonConvert.SerializeObject(validFilme);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var createResponse = await _httpClient.PostAsync("filmes", content);
            var createdFilmeResponse = JsonConvert.DeserializeObject<FilmeResponseDTO>(await createResponse.Content.ReadAsStringAsync());

            var updatedFilme = new FilmeRequestDTO
            {
                Titulo = "fffffffffff",
                Diretor = "hhhhhhhhhhhhhh",
                AnoLancamento = 2010,
                Genero = new List<string> { "Ação"},
                Sinopse = "iven"
            };

            var updatedJsonContent = JsonConvert.SerializeObject(updatedFilme);
            var updatedContent = new StringContent(updatedJsonContent, Encoding.UTF8, "application/json");

            var updateResponse = await _httpClient.PutAsync($"filmes/{createdFilmeResponse?.Id}", updatedContent);


            Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteFilme_ShouldReturnOk_WhenFilmeIsDeletedSuccessfully()
        {
  
            var validFilme = new FilmeRequestDTO
            {
                Titulo = "feellsk",
                Diretor = "ddddddeew",
                AnoLancamento = 1930,
                Genero = new List<string> {"Aventura"},
                Sinopse = "dddddddddddddd"
            };

            var jsonContent = JsonConvert.SerializeObject(validFilme);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var createResponse = await _httpClient.PostAsync("filmes", content);
            var createdFilmeResponse = JsonConvert.DeserializeObject<FilmeResponseDTO>(await createResponse.Content.ReadAsStringAsync());

            var deleteResponse = await _httpClient.DeleteAsync($"filmes/{createdFilmeResponse?.Id}");

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task SearchByTitle_ShouldReturnOk_WhenFilmesFound()
        {
            var validFilme = new FilmeRequestDTO
            {
                Titulo = "zzzzzzzzz",
                Diretor = "xxxxxxxx",
                AnoLancamento = 2015,
                Genero = new List<string> {"Aventura" },
                Sinopse = "dddddddddddddd"
            };

            var jsonContent = JsonConvert.SerializeObject(validFilme);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync("filmes", content);

            var searchResponse = await _httpClient.GetAsync($"filmes/search?title=zzzzzzzzz");

            Assert.Equal(HttpStatusCode.OK, searchResponse.StatusCode);
        }
movie
        [Fact]
        public async Task SearchByTitle_ShouldReturnNotFound_WhenNoFilmesFound()
        {
            var searchResponse = await _httpClient.GetAsync($"filmes/search?title=Inexistente");
            Assert.Equal(HttpStatusCode.NotFound, searchResponse.StatusCode);
        }
    }
}

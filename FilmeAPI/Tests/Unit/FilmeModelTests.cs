using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using Models;

namespace Tests.Unit.Models
{
    public class FilmeModelTests
    {
     
        [Fact]
        public void FilmeModel_Should_Create_Valid_Instance()
        {

            var filmeId = ObjectId.GenerateNewId().ToString();  
            var titulo = "A Origem";
            var diretor = "Christopher Nolan";
            var generos = new List<string> { "Ficção Científica", "Suspense" };
            var anoLancamento = 2010;
            var sinopse = "Um ladrão que rouba segredos corporativos.";

            var filme = new filmeModel
            {
                Id = ObjectId.Parse(filmeId),  
                Titulo = titulo,
                Diretor = diretor,
                Genero = generos,
                AnoLancamento = anoLancamento,
                Sinopse = sinopse
            };

            Assert.Equal(filmeId, filme.Id.ToString()); 
            Assert.Equal(titulo, filme.Titulo);
            Assert.Equal(diretor, filme.Diretor);
            Assert.Equal(generos, filme.Genero);
            Assert.Equal(anoLancamento, filme.AnoLancamento);
            Assert.Equal(sinopse, filme.Sinopse);
        }


        [Fact]
        public void FilmeModel_Should_Have_Required_Title()
        {
  
            var filme = new FilmeModel
            {
                Id = ObjectId.GenerateNewId(),
                Diretor = "Christopher Nolan",
                Genero = new List<string> { "Ficção Científica" },
                AnoLancamento = 2010,
                Sinopse = "Um ladrão que rouba segredos corporativos."
            };


            Assert.Throws<ValidationException>(() =>
            {
                if (string.IsNullOrEmpty(filme.Titulo))
                {
                    throw new ValidationException("Título é obrigatório.");
                }
            });
        }

        [Fact]
        public void FilmeModel_Should_Have_Required_Director()
        {

            var filme = new FilmeModel
            {
                Id = ObjectId.GenerateNewId(),
                Titulo = "A Origem",
                Genero = new List<string> { "Ficção Científica" },
                AnoLancamento = 2010,
                Sinopse = "Um ladrão que rouba segredos corporativos."
            };

            Assert.Throws<ValidationException>(() =>
            {
                if (string.IsNullOrEmpty(filme.Diretor))
                {
                    throw new ValidationException("Diretor é obrigatório.");
                }
            });
        }


        [Theory]
        [InlineData(1899)]
        [InlineData(2025)]
        public void FilmeModel_Should_Throw_Error_For_Invalid_ReleaseYear(int anoLancamento)
        {
            // Arrange
            var filme = new FilmeeModel
            {
                Id = ObjectId.GenerateNewId(),
                Titulo = "A Origem",
                Diretor = "Christopher Nolan",
                Genero = new List<string> { "Ficção Científica" },
                AnoLancamento = anoLancamento,
                Sinopse = "Um ladrão que rouba segredos corporativos."
            };

            Assert.Throws<ValidationException>(() =>
            {
                if (anoLancamento < 1900 || anoLancamento > 2024)
                {
                    throw new ValidationException("Ano de lançamento deve estar entre 1900 e 2024.");
                }
            });
        }

        [Fact]
        public void FilmeModel_Should_Have_Synopsis_Maximum_300_Characters()
        {

            var filme = new FilmeModel
            {
                Id = ObjectId.GenerateNewId(),
                Titulo = "A Origem",
                Diretor = "Christopher Nolan",
                Genero = new List<string> { "Ficção Científica" },
                AnoLancamento = 2010,
                Sinopse = new string('A', 501) 
            };

            Assert.Throws<ValidationException>(() =>
            {
                if (filme.Sinopse.Length > 300)
                {
                    throw new ValidationException("Sinopse não pode ter mais de 300 caracteres.");
                }
            });
        }
    }
}

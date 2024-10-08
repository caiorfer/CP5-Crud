using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models;
using Swashbuckle.AspNetCore.Annotations;

namespace DTOs
{
   
    [SwaggerSchema(Title = "FilmeResponse",Description = "Representa o DTO")]
    public class FilmeResponseDTO
    {
        
        [SwaggerSchema(
            Title = "ID do Filme",
            Description = "Identificador do filme.")]
        [Required(ErrorMessage = "O ID é obrigatório.")]
        public string? Id { get; set; }

        [SwaggerSchema(Title = "Titulo", Description = "Titulo do filme.")]
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(80, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string? Titulo { get; set; }

        [SwaggerSchema(Title = "Diretor", Description = "Nome do diretor do filme.")]
        [Required(ErrorMessage = "O diretor é obrigatório.")]
        [StringLength(30, ErrorMessage = "O nome do diretor não pode exceder 50 caracteres.")]
        public string? Diretor { get; set; }

        [SwaggerSchema(Title = "Gênero", Description = "Lista de gêneros associados ao filme.")]
        [Required(ErrorMessage = "Pelo menos um gênero é obrigatório.")]
        public ICollection<string>? Genero { get; set; }

        [SwaggerSchema(
            Title = "Ano de Lançamento",
            Description = "Ano de lançamento do filme. O valor deve estar entre 1900 e 2024.")]
        [Required(ErrorMessage = "O ano de lançamento é obrigatrio.")]
        [Range(1900, 2024, ErrorMessage = "O ano de lançamento deve estar entre 1900 e 2024.")]
        public int AnoLancamento { get; set; }

        [SwaggerSchema(Title = "Sinopse", Description = "Sinopse do filme. Limite de 300 caracteres.")]
        [StringLength(300, ErrorMessage = "A sinopse não pode exceder 300 caracteres.")]
        public string? Sinopse { get; set; }
        
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models
{
    public class FilmeModel : IFilmeModel
    { 
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } 

        [BsonElement("titulo")]
        [Required(ErrorMessage = "Coloque o Titulo.")]
        [StringLength(80, ErrorMessage = "O título não pode passar de 80 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [BsonElement("diretor")]
        [Required(ErrorMessage = "Todo Filme contem ao menos um diretor")]
        [StringLength(30, ErrorMessage = "O nome do diretor não pode passar 30 de caracteres")]
        public string Diretor { get; set; } = string.Empty;

        [BsonElement("genero")]
        [Required(ErrorMessage = "Insira um genero.")]
        public ICollection<string> Genero { get; set; } = new List<string>();

        [BsonElement("Lancamento")]
        [Range(1900, 2024, ErrorMessage = "O ano de lançamento deve estar entre 1900 e 2024.")]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public int AnoLancamento { get; set; }

        [BsonElement("sinopse")]
        [StringLength(300, ErrorMessage = "A sinopse não pode ter mais que 300 caracteres.")]
        public string Sinopse { get; set; } = string.Empty;

        public FilmeModel() {}
    }
}

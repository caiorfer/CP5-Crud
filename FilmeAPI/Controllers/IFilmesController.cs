using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using DTOs;

namespace Controllers
{
    public interface IFilmesController
    {
        Task<ActionResult<IEnumerable<FilmeResponseDTO>>> GetAllFilmes();

        Task<ActionResult<FilmeResponseDTO>> GetFilmeById(string id);

        Task<ActionResult> CreateFilme(FilmeRequestDTO filmeDto);

        Task<ActionResult<string>> UpdateFilme(string id, FilmeRequestDTO filmeDto);

        Task<ActionResult<string>> DeleteFilme(string id);

        Task<ActionResult<IEnumerable<FilmeResponseDTO>>> GetFilmeByYear(int Year);

        Task<ActionResult<IEnumerable<FilmeResponseDTO>>> SearchByTitle(string title);
    }
}

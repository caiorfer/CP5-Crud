using Microsoft.AspNetCore.Mvc;
using Repositories;
using Swashbuckle.AspNetCore.Annotations;
using DTOs;
using AutoMapper;
using Models;

namespace Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    public class FilmesController : ControllerBase, IFilmesController
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IMapper _mapper;

        public FilmesController(IFilmeRepository filmeRepository, IMapper mapper)
        {
            _filmeRepository = filmeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Tags("Ler")]
        [SwaggerOperation(Summary = "Obter todos os filmes",
            Description = "Retorna uma lista de todos os filmes disponíveis no catálogo.")]
        [ProducesResponseType(typeof(IEnumerable<FilmeResponseDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<FilmeResponseDTO>>> GetAllFilme()
        {
            var filmes = await _filmeRepository.GetAllAsync();
            var filmeDtos = _mapper.Map<List<FilmeResponseDTO>>(filme);
            return Ok(filmeDtos);
        }

        [HttpGet("{id}")]
        [Tags("Ler")]
        [SwaggerOperation(Summary = "Obter filme por ID",
            Description = "Retorna um filme")]
        [ProducesResponseType(typeof(FilmeResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FilmeResponseDTO>> GetFilmeById(string id)
        {
            var filme = await _filmeRepository.GetByIdAsync(id);
            if (filme == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<FilmeResponseDTO>(filme));
        }

        [HttpPost]
        [Tags("Criar")]
        [SwaggerOperation(Summary = "Adicionar um novo filme",
            Description = "Cria um novo filme")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateFilme([FromBody] FilmeRequestDTO filmeDto)
        {
            var filme = _mapper.Map<FilmeModel>(filmeDto);
            await _filmeRepository.CreateAsync(filme);

            var filmeResponseDto = _mapper.Map<FilmeResponseDTO>(filme);
            return CreatedAtAction(nameof(GetFilmeById), new { id = filme.Id.ToString() }, filmeResponseDto);
        }

        [HttpPut("{id}")]
        [Tags("Atualizar")]
        [SwaggerOperation(Summary = "Atualizar um filme",
            Description = "Atualiza um filme existente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> UpdateFilme(string id, [FromBody] FilmeRequestDTO filmeDto)
        {
            var existingFilme = await _filmeRepository.GetByIdAsync(id);
            if (existingFilme == null)
            {
                return NotFound();
            }

            var filmeToUpdate = _mapper.Map<FilmeModel>(filmeDto);
            filmeToUpdate.Id = existingFilme.Id;

            await _filmeRepository.UpdateAsync(id, filmeToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Tags("Deletar")]
        [SwaggerOperation(Summary = "Excluir um filme",
            Description = "Remove um filme existente")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> DeleteFilme(string id)
        {
            var existingFilme = await _filmeRepository.GetByIdAsync(id);
            if (existingFilme == null)
            {
                return NotFound();
            }

            await _filmeRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("year/{year}")]
        [Tags("Ler")]
        [SwaggerOperation(Summary = "Obter filmes por ano",
            Description = "Retorna uma lista de filmes")]
        [ProducesResponseType(typeof(IEnumerable<FilmeResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FilmeResponseDTO>>> GetFilmesByYear(int year)
        {
            var filmes = await _filmeRepository.GetFilmeByYearAsync(Year);
            if (filmes == null || !filmes.Any())
            {
                return NotFound();
            }

            var filmeDtos = _mapper.Map<List<FilmeResponseDTO>>(filmes);
            return Ok(filmeDtos);
        }

        [HttpGet("search")]
        [Tags("Ler")]
        [SwaggerOperation(Summary = "Buscar filmes por título",
            Description = "Retorna uma lista de filmes")]
        [ProducesResponseType(typeof(IEnumerable<FilmeResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<FilmeResponseDTO>>> SearchByTitle([FromQuery] string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return BadRequest("não pode deixar vazio.");
            }

            var filme = await _filmeRepository.GetFilmesByTitleAsync(title);
            if (filme == null || !filmes.Any())
            {
                return NotFound("Nenhum filme encontrado");
            }

            var filmeDtos = _mapper.Map<List<FilmeResponseDTO>>(filmes);
            return Ok(filmeDtos);
        }
    }
}

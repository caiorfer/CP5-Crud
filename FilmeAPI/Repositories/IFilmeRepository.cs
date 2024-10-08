using Models;

namespace Repositories
{
    public interface IFilmeRepository : IBaseRepository<FilmeModel>
    {
        Task<IEnumerable<FilmeModel>> GetFilmesesByYearAsync(int Year);
        
        Task<IEnumerable<FilmeModel>> GetFilmesByTitleAsync(string Title);
    }
}

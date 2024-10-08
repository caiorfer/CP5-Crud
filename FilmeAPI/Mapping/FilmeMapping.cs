using AutoMapper;
using DTOs;
using Models;

namespace Mapping
{
    public class FilmeMappingPerfil : Perfil
    {

        public FilmeMappingPerfil()
        {
            CreateMap<FilmeRequestDTO, FilmeModel>();

            CreateMap<FilmeModel, FilmeResponseDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
        }
    }
}
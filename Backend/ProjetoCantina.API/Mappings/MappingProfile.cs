using AutoMapper;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Models;

namespace ProjetoCantina.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        CreateMap<Caixa, CaixaDTO>().ReverseMap();
        CreateMap<FluxoCaixa, FluxoCaixaDTO>().ReverseMap();
        CreateMap<Venda, VendaDTO>().ReverseMap();
    }
}

using AutoMapper;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.UnitOfWork;

namespace ProjetoCantina.API.Services.Service;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoriaDTO>?> GetAllCategoriasAsync()
    {
        var categorias = await _unitOfWork
            .CategoriaRepository
                .GetAllAsync<Categoria>(
                    orderBy: c => c.OrderByDescending(c => c.CategoriaID)                    
                );

        return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
    }
    
    public async Task<IEnumerable<CategoriaDTO>?> GetAllCategoriasComProdutosAsync()
    {
        var categorias = await _unitOfWork
            .CategoriaRepository
                .GetAllAsync<Categoria>(
                   includeProperties: "Produtos",
                   orderBy: c => c.OrderBy(c => c.Nome)                    
                );

        return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
    }

    public async Task<CategoriaDTO?> GetCategoriaByIdAsync(int categoriaID)
    {
        var categoria = await _unitOfWork
            .CategoriaRepository
                .GetByIdAsync(
                    firstOrDefault: c => c.CategoriaID == categoriaID                    
                );

        return _mapper.Map<CategoriaDTO>(categoria);
    } 
    
    public async Task<CategoriaDTO?> GetCategoriaComProdutoByIdAsync(int categoriaID)
    {
        var categoria = await _unitOfWork
            .CategoriaRepository
                .GetByIdAsync(
                    includeProperties: "Produtos",
                    firstOrDefault: c => c.CategoriaID == categoriaID                    
                );

        return _mapper.Map<CategoriaDTO>(categoria);
    }

    public async Task<bool> InsertCategoriaAsync(CategoriaDTO categoriaDTO)
    {
        var categoria = _mapper.Map<Categoria>(categoriaDTO);

        var result = await _unitOfWork.CategoriaRepository.InsertAsync(categoria);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

    public async Task<bool> UpdateCategoriaAsync(CategoriaDTO categoriaDTO)
    {
        var categoria = _mapper.Map<Categoria>(categoriaDTO);

        var result = _unitOfWork.CategoriaRepository.Update(categoria);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

    public async Task<bool> DeleteCategoriaAsync(int categoriaID)
    {
        var result = await _unitOfWork
            .CategoriaRepository
                .DeleteAsync(
                    c => c.CategoriaID == categoriaID
                );

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

}

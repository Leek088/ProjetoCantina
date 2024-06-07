using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.UnitOfWork;

namespace ProjetoCantina.API.Services.Service;

public class ProdutoService : IProdutoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProdutoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProdutoDTO>> GetAllProdutosAsync()
    {
        var produtos = await _unitOfWork
           .ProdutoRepository
               .GetAllAsync<Produto>(
                   orderBy: p => p.OrderByDescending(p => p.ProdutoID)
               );

        return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
    }

    public async Task<IEnumerable<ProdutoDTO>> GetAllProdutosComCategoriaAsync()
    {
        var produtos = await _unitOfWork
           .ProdutoRepository
               .GetAllAsync<Produto>(
                   includeProperties: "Categoria",
                   orderBy: p => p.OrderBy(p => p.Nome)
               );

        return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
    }

    public async Task<IEnumerable<ProdutoDTO>> GetAllProdutosComVendasAsync()
    {
        var produtos = await _unitOfWork
           .ProdutoRepository
               .GetAllAsync<Produto>(
                   includeProperties: "Vendas",
                   orderBy: p => p.OrderBy(p => p.Nome)
               );

        return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
    }

    public async Task<IEnumerable<ProdutoDTO>?> GetAllProdutosComCategoriaVendasAsync()
    {
        var produtos = await _unitOfWork
            .ProdutoRepository
                .GetAllAsync<Produto>(
                    includeProperties: "Categoria, Vendas",
                    orderBy: p => p.OrderBy(p => p.Nome)
                );

        return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
    }

    public async Task<ProdutoDTO?> GetProdutoByIdAsync(int produtoID)
    {
        var produto = await _unitOfWork
            .ProdutoRepository
                .GetByIdAsync(
                    firstOrDefault: p => p.ProdutoID == produtoID
                );

        return _mapper.Map<ProdutoDTO?>(produto);
    }

    public async Task<ProdutoDTO?> GetProdutoComCategoriaByIdAsync(int produtoID)
    {
        var produto = await _unitOfWork
            .ProdutoRepository
                .GetByIdAsync(
                    firstOrDefault: p => p.ProdutoID == produtoID,
                    includeProperties: "Categoria"
                );

        return _mapper.Map<ProdutoDTO?>(produto);
    }

    public async Task<ProdutoDTO?> GetProdutoComVendasByIdAsync(int produtoID)
    {
        var produto = await _unitOfWork
            .ProdutoRepository
                .GetByIdAsync(
                    firstOrDefault: p => p.ProdutoID == produtoID,
                    includeProperties: "Vendas"
                );

        return _mapper.Map<ProdutoDTO?>(produto);
    } 
    
    public async Task<ProdutoDTO?> GetProdutoComCategoriaVendasByIdAsync(int produtoID)
    {
        var produto = await _unitOfWork
            .ProdutoRepository
                .GetByIdAsync(
                    firstOrDefault: p => p.ProdutoID == produtoID,
                    includeProperties: "Categoria, Vendas"
                );

        return _mapper.Map<ProdutoDTO?>(produto);
    }    

    public async Task<IEnumerable<ProdutoDTO>?> GetProdutoByNomeAsync(string nomeProduto)
    {
        var produtos = await _unitOfWork
            .ProdutoRepository
                .GetAllAsync<Produto>(
                    where: p => EF.Functions.Like(p.Nome, $"%{nomeProduto}%"),
                    orderBy: p => p.OrderBy(p => p.Nome)
                );

        return _mapper.Map<IEnumerable<ProdutoDTO>?>(produtos);
    }

    public async Task<IEnumerable<ProdutoDTO>?> GetProdutoComCategoriaByNomeAsync(string nomeProduto)
    {
        var produtos = await _unitOfWork
            .ProdutoRepository
                .GetAllAsync<Produto>(
                    where: p => EF.Functions.Like(p.Nome, $"%{nomeProduto}%"),
                    includeProperties: "Categoria",
                    orderBy: p => p.OrderBy(p => p.Nome)
                );

        return _mapper.Map<IEnumerable<ProdutoDTO>?>(produtos);
    }

    public async Task<IEnumerable<ProdutoDTO>?> GetProdutoComVendasByNomeAsync(string nomeProduto)
    {
        var produtos = await _unitOfWork
            .ProdutoRepository
                .GetAllAsync<Produto>(
                    where: p => EF.Functions.Like(p.Nome, $"%{nomeProduto}%"),
                    includeProperties: "Vendas",
                    orderBy: p => p.OrderBy(p => p.Nome)
                );

        return _mapper.Map<IEnumerable<ProdutoDTO>?>(produtos);
    }
    
    public async Task<IEnumerable<ProdutoDTO>?> GetProdutoComCategoriaVendasByNomeAsync(string nomeProduto)
    {
        var produtos = await _unitOfWork
            .ProdutoRepository
                .GetAllAsync<Produto>(
                    where: p => EF.Functions.Like(p.Nome, $"%{nomeProduto}%"),
                    includeProperties: "Categoria, Vendas",
                    orderBy: p => p.OrderBy(p => p.Nome)
                );

        return _mapper.Map<IEnumerable<ProdutoDTO>?>(produtos);
    }

    public async Task<bool> InsertProdutoAsync(ProdutoDTO produtoDTO)
    {
        var produto = _mapper.Map<Produto>(produtoDTO);

        var result = await _unitOfWork.ProdutoRepository.InsertAsync(produto);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

    public async Task<bool> UpdatePrdoutoAsync(ProdutoDTO produtoDTO)
    {
        var produto = _mapper.Map<Produto>(produtoDTO);

        var result = _unitOfWork.ProdutoRepository.Update(produto);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }
    public async Task<bool> DeleteProdutoAsync(int produtoID)
    {
        var result = await _unitOfWork
            .ProdutoRepository
                .DeleteAsync(
                    p => p.ProdutoID == produtoID
                );

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

}

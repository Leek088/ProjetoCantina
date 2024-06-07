using AutoMapper;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.UnitOfWork;

namespace ProjetoCantina.API.Services.Service;

public class CaixaService : ICaixaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CaixaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CaixaDTO>?> GetAllCaixasAsync()
    {
        var caixas = await _unitOfWork
            .CaixaRepository
                .GetAllAsync<Caixa>(
                    orderBy: c => c.OrderByDescending(c => c.CaixaID),
                    includeProperties: "Usuario, FluxoCaixa"
                );

        return _mapper.Map<IEnumerable<CaixaDTO>>(caixas);
    }

    public async Task<CaixaDTO?> GetCaixaByCodigoUnicoAsync(string codigoUnico)
    {
        var caixa = await _unitOfWork
            .CaixaRepository
                .GetByIdAsync(
                    firstOrDefault: c => c.CodigoUnico == codigoUnico,
                    includeProperties: "Usuario"
                );

        return _mapper.Map<CaixaDTO>(caixa);
    }

    public async Task<CaixaDTO?> GetCaixaByIdAsync(int caixaID)
    {
        var caixa = await _unitOfWork
          .CaixaRepository
              .GetByIdAsync(
                  firstOrDefault: c => c.CaixaID == caixaID,
                  includeProperties: "Usuario, FluxoCaixa, Vendas.Produto"
              );

        return _mapper.Map<CaixaDTO>(caixa);
    }

    public async Task<bool> InsertCaixaAsync(CaixaDTO caixaDTO)
    {
        var caixa = _mapper.Map<Caixa>(caixaDTO);

        var result = await _unitOfWork.CaixaRepository.InsertAsync(caixa);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

    public async Task<bool> UpdateCaixaAsync(CaixaDTO caixaDTO)
    {
        var caixa = _mapper.Map<Caixa>(caixaDTO);

        var result = _unitOfWork.CaixaRepository.Update(caixa);

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }

    public async Task<bool> DeleteCaixaAsync(int caixaID)
    {
        var result = await _unitOfWork
             .CaixaRepository
                 .DeleteAsync(
                    firstOrDefault: c => c.CaixaID == caixaID
                 );

        if (result)
        {
            result = await _unitOfWork.SaveChangeAsync();
        }

        return result;
    }
}

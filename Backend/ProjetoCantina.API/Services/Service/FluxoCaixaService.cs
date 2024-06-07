using AutoMapper;
using ProjetoCantina.API.DTOs;
using ProjetoCantina.API.Models;
using ProjetoCantina.API.Services.Interfaces;
using ProjetoCantina.API.UnitOfWork;

namespace ProjetoCantina.API.Services.Service;

public class FluxoCaixaService : IFluxoCaixaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FluxoCaixaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FluxoCaixaDTO>?> GetAllFluxosCaixasAsync()
    {
        var fluxosCaixas = await _unitOfWork
            .FluxoCaixaRepository
                .GetAllAsync<FluxoCaixa>();

        return _mapper.Map<IEnumerable<FluxoCaixaDTO>>(fluxosCaixas);
    }

    public async Task<FluxoCaixaDTO?> GetFluxoCaixaByIdAsync(int fluxoCaixaID)
    {
        var fluxoCaixa = await _unitOfWork
            .FluxoCaixaRepository
                .GetByIdAsync(
                    firstOrDefault: fc => fc.FluxoCaixaID == fluxoCaixaID
                );

        return _mapper.Map<FluxoCaixaDTO>(fluxoCaixa);
    }

    public async Task<bool> InsertFluxoCaixaAsync(FluxoCaixaDTO fluxoCaixaDTO)
    {
        var fluxocaixa = _mapper.Map<FluxoCaixa>(fluxoCaixaDTO);

        var result = await _unitOfWork.FluxoCaixaRepository.InsertAsync(fluxocaixa);

        if (result)
            result = await _unitOfWork.SaveChangeAsync();

        return result;
    }

    public async Task<bool> UpdateFluxoCaixaAsync(FluxoCaixaDTO fluxoCaixaDTO)
    {
        var fluxocaixa = _mapper.Map<FluxoCaixa>(fluxoCaixaDTO);

        var result = _unitOfWork.FluxoCaixaRepository.Update(fluxocaixa);

        if (result)
            result = await _unitOfWork.SaveChangeAsync();

        return result;
    }
    public async Task<bool> DeleteFluxoCaixaAsync(int fluxoCaixaID)
    {
        var result = await _unitOfWork
            .FluxoCaixaRepository
                .DeleteAsync(
                    firstOrDefault: fc => fc.FluxoCaixaID == fluxoCaixaID
                );

        if (result)
            result = await _unitOfWork.SaveChangeAsync();

        return result;
    }
}
